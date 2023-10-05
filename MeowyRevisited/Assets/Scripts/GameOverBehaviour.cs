using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{
    public void Restart(int GameScene) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameScene);
    }
    public void Home(int MainMenu)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }
}
