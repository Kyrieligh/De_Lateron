using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
    }

    // Update is called once per frame
    public void TriggerGameOver()
    {
        gameOverScreen.SetActive(true);

        Time.timeScale = 0f; //Time.timeScale = 0f; agar membuat gamenya berhenti
    }

    public void Restart()
    {
        Time.timeScale = 1f; //Time.timeScale = 0f; cause make game load

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0); //why this code is long ? cause this code just use for current scene 
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);// why this code isn't long ? cause we know we will back to load scene first scene mean scene 0
    }
}
