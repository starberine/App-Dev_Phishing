using UnityEngine;
using UnityEngine.UI;

public class GameMenuView : View
{
    [SerializeField] private Button _bestiaryButton;
    // [SerializeField] private AudioSource _audioSource;
    // [SerializeField] private AudioClip _buttonClickSFX;

    public override void Initialize()
    {
        _bestiaryButton.onClick.AddListener(OnBestiaryButtonClick);
    }

    private void OnBestiaryButtonClick()
    {
        ViewManager.Show<BestiaryMenuView>(); // Show Bestiary View
    }
}
