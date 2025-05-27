using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuView : View
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _settingsButton;

    public override void Initialize()
    {
        _playButton.onClick.AddListener(PlayGame);
        _exitButton.onClick.AddListener(ExitGame);
        _settingsButton.onClick.AddListener(OpenSettings);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your actual game scene name
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OpenSettings()
    {
        ViewManager.Show<SettingsMenuView>();
    }
}
