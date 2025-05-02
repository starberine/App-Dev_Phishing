using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game"); // Only visible in editor
    }
}
