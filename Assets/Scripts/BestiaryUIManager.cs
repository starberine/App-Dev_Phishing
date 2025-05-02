using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BestiaryUIManager : MonoBehaviour
{
    public GameObject bestiaryPanel;
    public Text fishListText; // Assign this to a Text UI inside the panel

    private bool isVisible = false;

    public void ToggleBestiary()
    {
        isVisible = !isVisible;
        bestiaryPanel.SetActive(isVisible);

        if (isVisible)
        {
            UpdateFishList();
        }
    }

    void UpdateFishList()
    {
        if (fishListText == null) return;

        var allFish = BestiaryManager.Instance.GetCaughtFish();
        StringBuilder sb = new StringBuilder("Caught Fish:\n");

        foreach (var fish in allFish)
        {
            sb.AppendLine("- " + fish);
        }

        fishListText.text = sb.ToString();
    }
}
