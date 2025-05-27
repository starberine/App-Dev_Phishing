using UnityEngine;
using UnityEngine.UI;

public class GameMenuView : View
{
    [SerializeField] private Button _bestiaryButton;

    public override void Initialize()
    {
        _bestiaryButton.onClick.AddListener(OnBestiaryButtonClick);
    }

    private void OnBestiaryButtonClick()
    {
        AudioManager.instance.PlayButtonPressSFX();   
        ViewManager.Show<BestiaryMenuView>();          
    }
}
