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
        _backButton.onClick.AddListener(() => ViewManager.ShowLast());

        _mainVolumeSlider.value = AudioManager.Instance.GetMainVolume();
        _bgmVolumeSlider.value = AudioManager.Instance.GetBGMVolume();
        _sfxVolumeSlider.value = AudioManager.Instance.GetSFXVolume();

        _mainVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetMainVolume);
        _bgmVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        _sfxVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}
