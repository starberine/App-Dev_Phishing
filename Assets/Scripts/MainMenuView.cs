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
        AudioManager.instance.PlayButtonPressSFX();
        SceneManager.LoadScene("GameScene");
    }

    private void ExitGame()
    {
        AudioManager.instance.PlayButtonPressSFX();
        Application.Quit();
    }
    
    private void OpenSettings()
    {
        AudioManager.instance.PlayButtonPressSFX();
        ViewManager.Show<SettingsMenuView>();
    }
}
