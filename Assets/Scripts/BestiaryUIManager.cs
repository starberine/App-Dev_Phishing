using UnityEngine;
using UnityEngine.UI;

public class BestiaryUIManager : MonoBehaviour
{
    public GameObject bestiaryPanel;
    public GameObject detailPanel;
    public Text detailText;

    private bool isVisible = false;

    public void ToggleBestiary()
    {
        isVisible = !isVisible;
        bestiaryPanel.SetActive(isVisible);
        detailPanel.SetActive(false);
    }

    // Attach this to the fish button manually in the Inspector
    public void OpenFishDetail()
    {
        bestiaryPanel.SetActive(false);
        detailPanel.SetActive(true);
        detailText.text = "Details for Fishy Fish"; // Placeholder
    }

    // Attach this to a back button inside the detail panel
    public void BackToList()
    {
        detailPanel.SetActive(false);
        bestiaryPanel.SetActive(true);
    }

    // Attach this to a 'Close Bestiary' button
    public void CloseBestiary()
    {
        bestiaryPanel.SetActive(false);
        detailPanel.SetActive(false);
        isVisible = false;
    }
}
