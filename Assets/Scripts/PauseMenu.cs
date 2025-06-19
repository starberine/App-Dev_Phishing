using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;

    private bool isPaused = false;

    void Start()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        if (AudioManager.instance != null)
             AudioManager.instance.PlayButtonPressSFX();
        Time.timeScale = 1f;
        isPaused = false;
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        if (AudioManager.instance != null)
             AudioManager.instance.PlayButtonPressSFX();
        Time.timeScale = 0f;
        isPaused = true;
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reset time before changing scenes
        SceneManager.LoadScene("MainMenuScene");
    }
}