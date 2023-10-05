using UnityEngine;
using TMPro;

public class PlayerControlBehaviour : MonoBehaviour
{
    //Vector2 pMovement;
    Rigidbody2D pBody;
    public Animator pAnim;
    SpriteRenderer pSprite;

    public float speed = 5f;
    public float playerJump = 5f;
    float currentplayerJump;
    float currentplayerSpeed; 
    public float powerJumpTimer = 0f;
    public float speedTimer = 0f;
    public float speedjumpTimer = 0f;
    public int CatLives = 3;
    public float pDirection = 1;
    float direction = 1;
    //bool crouch = false;

    public AudioSource aSource;
    public AudioClip jumpSfx;
    public AudioClip bulletSfx;
    public AudioClip pUpSfx;
    public AudioClip landingSfx;

    public Transform PlayerBullet;
    public GameObject shootBullet;
    public TextMeshProUGUI healthBar;
    public TextMeshProUGUI speedBar;
    public TextMeshProUGUI jumpBar;
    public TextMeshProUGUI combinedBar;
    public GameObject gameOver;
    public GameObject youWin;
    

    public bool isGrounded; // no longer checks and now checks instead in the animation sequence and in game

    void Start()
    {

        pBody = GetComponent<Rigidbody2D>();
        pAnim = GetComponent<Animator>();
        pSprite = GetComponent<SpriteRenderer>();


        currentplayerJump = playerJump;
        currentplayerSpeed = speed;
    }

    void Update()
    {
        
        PlayerMovement();
        PlayerBoost();
        PlayerShootingMechanic();
    }

    void PlayerMovement()
    {
        float pHorizontal = Input.GetAxis("Horizontal");
        pBody.velocity = new Vector2(pHorizontal * currentplayerSpeed, pBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            pBody.AddForce(Vector2.up * currentplayerJump, ForceMode2D.Impulse);
            pAnim.SetTrigger("Jump");
            aSource.PlayOneShot(jumpSfx);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            pSprite.flipX = false;

            pDirection = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            pSprite.flipX = true;

            pDirection = -1;
        }

        //animations for the player

        if (pHorizontal != 0)
        {
            pAnim.SetTrigger("Run");
        }
        else
        {
            pAnim.SetTrigger("Idle");
        }

        if (!isGrounded)
        {
            pAnim.SetBool("isGrounded", false);
        }
        else
        {
            pAnim.SetBool("isGrounded", true);
        }
    }

    public void PlayerShootingMechanic()
    {
        GameObject PBullet;

        if (Input.GetAxis("Horizontal") > 0)
        {
            PlayerBullet.position = transform.position + new Vector3(0.43f, 0, 0);

            direction = 1;
        }

        else if (Input.GetAxis("Horizontal") < 0)
        {
            PlayerBullet.position = transform.position - new Vector3(0.43f, 0, 0);

            direction = -1;
        }

        // key input

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.LeftBracket)) //slash is for wasd movement, Q is for arrow key movement, for your comfortability 
        {
            PBullet = Instantiate(shootBullet, PlayerBullet.position, Quaternion.identity);
            PBullet.GetComponent<BulletBehaviour>().speed *= direction;
            aSource.PlayOneShot(bulletSfx);
        }
    }

    void PlayerBoost()
    {
        if (powerJumpTimer > 0)
        {
            powerJumpTimer -= Time.deltaTime;
            jumpBar.text = "JumpT: " + Mathf.RoundToInt(powerJumpTimer).ToString();

            if (powerJumpTimer == 0)
            {
                powerJumpTimer = 0;
                currentplayerJump = playerJump;

            }
        }

        if (speedTimer > 0)
        {
            speedTimer -= Time.deltaTime;
            speedBar.text = "SpeedT: " + Mathf.RoundToInt(speedTimer).ToString();

            if (speedTimer == 0)
            {
                speedTimer = 0;
                currentplayerSpeed = speed;
            }

        }

        if (speedjumpTimer > 0)
        {
            speedjumpTimer -= Time.deltaTime;
            combinedBar.text = "Combined: " + Mathf.RoundToInt(speedjumpTimer).ToString();

            if (speedjumpTimer == 0)
            {
                speedjumpTimer = 0;
                currentplayerSpeed = speed;
                currentplayerJump = playerJump;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floorPlatform") {
            isGrounded = true;
            aSource.PlayOneShot(landingSfx);
        }

        if (collision.gameObject.tag.Equals("jumpPower"))
        {
            currentplayerJump = playerJump * 2;
            powerJumpTimer = 15;
            PlayerPowerUpBehaviour ppu = collision.gameObject.GetComponent<PlayerPowerUpBehaviour>();
            ppu.HidePU();
            aSource.PlayOneShot(pUpSfx);
        }

        if (collision.gameObject.tag.Equals("speedPower"))
        {
            currentplayerSpeed = speed * 2;
            speedTimer = 15;
            speedTimer = 15;
            PlayerPowerUpBehaviour ppu = collision.gameObject.GetComponent<PlayerPowerUpBehaviour>();
            ppu.HidePU();
            aSource.PlayOneShot(pUpSfx);

        }

        if (collision.gameObject.tag.Equals("speedjumpPower"))
        {
            currentplayerJump = playerJump * 2;
            currentplayerSpeed = speed * 2;
            speedjumpTimer = 20;
            PlayerPowerUpBehaviour ppu = collision.gameObject.GetComponent<PlayerPowerUpBehaviour>();
            ppu.HidePU();
            aSource.PlayOneShot(pUpSfx);

        }

        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            pAnim.SetTrigger("Hit");
            CatDead();
        }

        if (collision.collider.tag.Equals("EndFlag"))
        {
            gameObject.SetActive(false);
            youWin.SetActive(true);
            return;
        }

        if (collision.collider.tag.Equals("OtherEndFlag"))
        {
            gameObject.SetActive(false);
            gameOver.SetActive(true);
            return;

        }
        if (collision.collider.tag.Equals("EnemyDinoBullet") || collision.gameObject.tag.Equals("Enemy"))
        {
            pAnim.SetTrigger("Hit");
            CatDead();
        }
    }

   private void OnCollisionExit2D(Collision2D collision)
    {   
        if(collision.gameObject.tag == "floorPlatform")
        {
            isGrounded = false;
        }
    }
    
    public void LifeUpdate(int increment) //cat lives, collision for obstacles is too much still 
    {
        CatLives += increment;
        healthBar.text = CatLives.ToString();
    }

    public void CatDead()
    {
        if (CatLives == 0)
        {
            gameObject.SetActive(false);
            gameOver.SetActive(true); //will add later 
            return;
        }
        LifeUpdate(-1);
    }
}
    