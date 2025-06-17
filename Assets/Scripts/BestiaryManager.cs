using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BestiaryManager : MonoBehaviour
{
    public GameObject bestiaryPanel;

    public TMP_Text fishNameText;
    public TMP_Text fishScientificNameText;
    public TMP_Text fishLocationText;
    public TMP_Text fishDescriptionText;
    public Image fishImage;

    public List<FishData> allFish;

    private HashSet<string> caughtFishNames;

    private readonly Color visibleColor = Color.white;
    private readonly Color silhouetteTint = new Color(0.1f, 0.1f, 0.1f);

    void Start()
    {
        caughtFishNames = new HashSet<string>(BestiarySaveSystem.Load());
        Debug.Log($"[Init] Loaded caught fish: {string.Join(", ", caughtFishNames)}");

        // Dynamically find all FishButtonHandler components in scene (including inactive children of the panel)
        FishButtonHandler[] fishButtons = bestiaryPanel.GetComponentsInChildren<FishButtonHandler>(true);

        foreach (var button in fishButtons)
        {
            FishData fish = allFish.Find(f => f.fishName == button.fishName);
            if (fish != null)
            {
                Debug.Log($"[Init] Button '{button.fishName}' linked to FishData '{fish.fishName}'");

                // Cache sprite and tint
                bool isCaught = caughtFishNames.Contains(fish.fishName);
                button.fishIcon.sprite = fish.fishSprite;
                button.fishIcon.color = isCaught ? visibleColor : silhouetteTint;

                // Setup listener
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.GetComponent<Button>().onClick.AddListener(() => ShowFishInfo(fish));
            }
            else
            {
                Debug.LogWarning($"[Init] No FishData found for button '{button.fishName}'");
            }
        }
    }

    public void RefreshBestiary()
    {
        Debug.Log("[RefreshBestiary] Called");

        caughtFishNames = new HashSet<string>(BestiarySaveSystem.Load());
        FishButtonHandler[] fishButtons = bestiaryPanel.GetComponentsInChildren<FishButtonHandler>(true);

        foreach (var button in fishButtons)
        {
            FishData fish = allFish.Find(f => f.fishName == button.fishName);
            if (fish == null)
            {
                Debug.LogWarning($"[Refresh] No match found in allFish for '{button.fishName}'");
                continue;
            }

            bool isCaught = caughtFishNames.Contains(fish.fishName);
            Debug.Log($"[Refresh] Fish '{fish.fishName}': isCaught = {isCaught}");

            button.fishIcon.sprite = fish.fishSprite;
            button.fishIcon.color = isCaught ? visibleColor : silhouetteTint;
        }
        ClearFishInfo(); 
    }

    public void ShowFishInfo(FishData fish)
    {
        bool isCaught = caughtFishNames.Contains(fish.fishName);
        Debug.Log($"[ShowFishInfo] Showing '{fish.fishName}', isCaught: {isCaught}");

        fishNameText.text = isCaught ? fish.fishName : "???";
        fishScientificNameText.text = isCaught ? fish.fishScientificName : "???";
        fishLocationText.text = isCaught ? fish.fishLocation : "???";
        fishDescriptionText.text = isCaught ? fish.fishDescription : "???";

        fishImage.sprite = fish.fishSprite;
        fishImage.color = isCaught ? visibleColor : silhouetteTint;
    }

    public void ToggleBestiary(bool show)
    {
        Debug.Log($"[ToggleBestiary] show = {show}");

        if (show)
        {
            RefreshBestiary();
        }

        bestiaryPanel.SetActive(show);
    }

    private void ClearFishInfo()
    {
        fishNameText.text = "";
        fishScientificNameText.text = "";
        fishLocationText.text = "";
        fishDescriptionText.text = "";
        fishImage.sprite = null;
        fishImage.color = Color.clear;
    }
}
