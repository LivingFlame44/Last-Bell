using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    //public void Win()
    //{
    //    Time.timeScale = 0f;
    //    winPanel.SetActive(true);
    //}


    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameManager.currentLevel = 5;
        GameManager.tutorialDone = false;
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.sfxSource.Stop();
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameManager.currentLevel = 5;
        SceneManager.LoadScene("Real Level");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
