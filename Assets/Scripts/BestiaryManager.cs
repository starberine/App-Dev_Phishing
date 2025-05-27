using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BestiaryManager : MonoBehaviour
{
    public GameObject bestiaryPanel;
    public GameObject fishButtonPrefab;
    public Transform listContent;

    public TMP_Text fishNameText;
    public TMP_Text fishScientificNameText;
    public TMP_Text fishLocationText;
    public TMP_Text fishDescriptionText;
    public Image fishImage;

    public List<FishData> allFish;

    void Start()
    {
        foreach (var fish in allFish)
        {
            GameObject btn = Instantiate(fishButtonPrefab, listContent);
            btn.GetComponentInChildren<TMP_Text>().text = fish.fishName;
            btn.GetComponent<Button>().onClick.AddListener(() => ShowFishInfo(fish));
        }
    }

    public void ShowFishInfo(FishData fish)
    {
        fishNameText.text = fish.fishName;
        fishScientificNameText.text = fish.fishScientificName;
        fishLocationText.text = fish.fishLocation;
        fishDescriptionText.text = fish.fishDescription;
        fishImage.sprite = fish.fishSprite;
    }

    public void ToggleBestiary(bool show)
    {
        bestiaryPanel.SetActive(show);
    }
}
