using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool isPause;
    // Start is called before the first frame update

    void Start()
    {
        isPause = false;
    }

    void Update() //this is for using the escape button to escape to the pause menu 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPause = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPause = false;
        }

        // wont trigger because it interferes with the first script, and its buggy with another type of code that i tried to use, will fix soon.
    }
    
    public void Pause() //this one is for the x button for the pause menu
    {   
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home(int MainMenu)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }   

    public void Restart(int GameScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameScene);
    }
}
