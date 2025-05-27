using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuView : View
{
    [SerializeField] private Button _backButton;

    [SerializeField] private Slider _mainVolumeSlider;
    [SerializeField] private Slider _bgmVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    public override void Initialize()
    {
        _backButton.onClick.AddListener(() =>
        {
            AudioManager.instance.PlayButtonPressSFX();  // Play button press sound
            ViewManager.ShowLast();                       // Then navigate back
        });

        _mainVolumeSlider.value = AudioManager.instance.GetMasterVolume();
        _bgmVolumeSlider.value = AudioManager.instance.GetBGMVolume();
        _sfxVolumeSlider.value = AudioManager.instance.GetSFXVolume();

        _mainVolumeSlider.onValueChanged.AddListener(AudioManager.instance.SetMasterVolume);
        _bgmVolumeSlider.onValueChanged.AddListener(AudioManager.instance.SetBGMVolume);
        _sfxVolumeSlider.onValueChanged.AddListener(AudioManager.instance.SetSFXVolume);
    }
}
