using UnityEngine;
using UnityEngine.UI;

public class BestiaryMenuView : View
{
    [SerializeField] private Button _backButton;

    public override void Initialize()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnBackButtonClick()
    {
        //AudioManager.instance.PlayButtonPressSFX();  
        ViewManager.ShowLast();                     
    }
}
