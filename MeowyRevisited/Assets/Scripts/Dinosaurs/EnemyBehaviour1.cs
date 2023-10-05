using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour1 : MonoBehaviour
{
    Rigidbody2D dinoRigidBody;
    public SpriteRenderer dinoSprite;
    Animator dinoAnim;

    public float movementSpeed = 2.5f;
    public int health = 4;
    public float cMovementSpeed;
    public Transform isGroundedCheck;
    public LayerMask groundLayer;
    public bool mustPatrol;
    private bool mustTurn;

    //public AudioSource DinoSounds;
    //public AudioClip attackSfx;
    //public AudioClip hitSfx;
    

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        cMovementSpeed = movementSpeed;
        dinoRigidBody = GetComponent<Rigidbody2D>();
        dinoSprite = GetComponent<SpriteRenderer>();
        dinoAnim = GetComponent<Animator>();  

    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        if (health == 0)
        {
            DinoDeath();
        }
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(isGroundedCheck.position, 0.1f, groundLayer);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("jumpPower") || collision.gameObject.tag.Equals("Destructable") || collision.gameObject.tag.Equals("speedjumpPower") || collision.gameObject.tag.Equals("speedPower") || collision.gameObject.tag.Equals("Obstacle")) {
           
            Flip();

        }
    }
    public void Patrol()
    {
        if(mustTurn)
        {
            Flip();
        }

        dinoRigidBody.velocity = new Vector2(cMovementSpeed * Time.fixedDeltaTime, dinoRigidBody.velocity.y);
        dinoAnim.SetTrigger("Run");
    }

    public void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        cMovementSpeed *= -1;
        movementSpeed *= -1;
        mustPatrol = true;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            dinoAnim.SetTrigger("Idle");
            cMovementSpeed = 0;
            //DinoSounds.PlayOneShot(attackSfx); //won't play?
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            dinoAnim.SetTrigger("Run");
            cMovementSpeed = movementSpeed;
        }
    }
    public void DinoGreenHit()
    {
        if (health > 0)
        {
            health -= 1;
            dinoAnim.SetTrigger("DinoGreenHit");
            //DinoSounds.PlayOneShot(hitSfx);
        }
    }


    void DinoDeath()
    {   
        Destroy(gameObject);
    }
}
