using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuView : View
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _resetButton;

    public override void Initialize()
    {
        _playButton.onClick.AddListener(PlayGame);
        _exitButton.onClick.AddListener(ExitGame);
        _settingsButton.onClick.AddListener(OpenSettings);
        _resetButton.onClick.AddListener(ResetBestiaryProgress);
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
    
    private void ResetBestiaryProgress()
    {
        AudioManager.instance.PlayButtonPressSFX();
        BestiarySaveSystem.Clear();
        CelebrationSaveSystem.ResetCelebrationFlag();
        Debug.Log("Bestiary save cleared.");
    }
}
