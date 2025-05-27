using UnityEngine;
using UnityEngine.UI;

public class BestiaryMenuView : View
{
    [SerializeField] private Button _backButton;
    // [SerializeField] private AudioSource _audioSource;
    // [SerializeField] private AudioClip _buttonClickSFX;

    public override void Initialize()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnBackButtonClick()
    {
        ViewManager.ShowLast(); // Go back to previous view
    }
}
