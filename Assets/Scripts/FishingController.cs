using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class FishingController : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform spawnPoint;
    public TMP_Text popupText;
    public TMP_InputField inputField;
    public FishData[] availableFish;
    public string[] fishingWords = { "catch", "hook", "bait", "reel", "tug" };

    private bool isInPond = false;
    private bool isFishing = false;
    private bool canFish = true;
    private PlayerController playerController;
    private string currentWord = "";

    [Header("Floating Prompt")]
    public TextMeshPro floatingPromptText;
    public float fadeSpeed = 2f;
    private float currentAlpha = 0f;
    private Camera mainCam;
    private bool enteredManually = false;
    [Range(0, 4)] public int islandIndex = 0;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        mainCam = Camera.main;

        if (inputField != null)
        {
            inputField.gameObject.SetActive(false);
        }

        if (floatingPromptText != null)
        {
            SetPromptAlpha(0f); // Hide initially
        }
        PortalManager portalManager = FindObjectOfType<PortalManager>();
        if (portalManager != null)
        {
                portalManager.UpdatePortalVisibility();
        }
    }

    void Update()
    {
        if (isInPond && !isFishing && canFish && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FishingRoutine());
        }

        if (floatingPromptText != null)
        {
            floatingPromptText.transform.rotation = Quaternion.LookRotation(
                floatingPromptText.transform.position - mainCam.transform.position
            );

            bool inputActive = inputField != null && inputField.gameObject.activeInHierarchy;
            float targetAlpha = (isInPond && !isFishing && !inputActive && canFish) ? 1f : 0f;
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
            SetPromptAlpha(currentAlpha);
        }

        // Detect Enter key ONLY while input is active
        if (isFishing && inputField != null && inputField.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            enteredManually = true;
            CheckTypedWord(inputField.text);
        }
    }

    System.Collections.IEnumerator FishingRoutine()
    {
        isFishing = true;
        canFish = false;

        if (playerController != null)
            playerController.isFrozen = true;

        ShowPopup("fishing...");
        float waitTime = Random.Range(1f, 3f);
        yield return new WaitForSeconds(waitTime);

        currentWord = fishingWords[Random.Range(0, fishingWords.Length)];
        ShowPopup("Type: " + currentWord);

        if (inputField != null)
        {
            inputField.text = "";
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
        }

        float timeout = 5f;
        float timer = 0f;

        while (timer < timeout)
        {
            if (!isFishing) break;
            timer += Time.deltaTime;
            yield return null;
        }

        if (isFishing)
        {
            ShowPopup("Too slow!");
            inputField.gameObject.SetActive(false);
            EndFishing(false);
        }
    }

    void CheckTypedWord(string typed)
    {
        if (!isFishing) return;

        // Only proceed if Enter was pressed (Input.GetKeyDown won't work here, so we set a flag manually)
        if (!enteredManually) // <-- We'll define this flag shortly
            return;

        inputField.gameObject.SetActive(false);

        if (typed.Trim().ToLower() == currentWord.ToLower())
        {
            ShowPopup("Success!");
            Fish();
            EndFishing(true);
        }
        else
        {
            ShowPopup("Failed!");
            EndFishing(false);
        }

        enteredManually = false; // Reset
    }

    void EndFishing(bool success)
    {
        if (playerController != null)
            playerController.isFrozen = false;

        isFishing = false;

        // Begin cooldown
        StartCoroutine(FishingCooldownRoutine());
    }

    IEnumerator FishingCooldownRoutine()
    {
        yield return new WaitForSeconds(3f);
        canFish = true;
    }

    void Fish()
    {
        if (availableFish.Length == 0 || spawnPoint == null)
            return;

        FishData selectedFish = GetFishForIsland();

        if (selectedFish.fishModel != null)
        {
            GameObject fish = Instantiate(selectedFish.fishModel, spawnPoint.position, Quaternion.Euler(0, 90, 0));
            fish.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(fish, 2f);

            Debug.Log("caught: " + selectedFish.fishName);
            ShowPopup("caught: " + selectedFish.fishName);

            if (AudioManager.instance != null)
                AudioManager.instance.PlayFishingCaughtSFX();

        SaveCaughtFish(selectedFish.fishName);
        }
        else
        {
            Debug.LogWarning("No model assigned for " + selectedFish.fishName);
        }
    }

    private FishData GetFishForIsland()
    {
        int fishableCount = Mathf.Clamp((islandIndex + 1) * 2, 0, availableFish.Length);
        return availableFish[Random.Range(0, fishableCount)];
    }

    public void SetIsland(int island)
    {
        islandIndex = island;
    }

    void ShowPopup(string message)
    {
        if (popupText != null)
        {
            popupText.text = message;
            popupText.gameObject.SetActive(true);
            CancelInvoke("HidePopup");
            Invoke("HidePopup", 2f);
        }
    }

    void HidePopup()
    {
        if (popupText != null)
        {
            popupText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pond"))
        {
            isInPond = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pond"))
        {
            isInPond = false;
        }
    }

    void SaveCaughtFish(string fishName)
    {
        List<string> savedFish = BestiarySaveSystem.Load();

        if (!savedFish.Contains(fishName))
        {
            savedFish.Add(fishName);
            BestiarySaveSystem.Save(savedFish);

            BestiaryManager manager = FindObjectOfType<BestiaryManager>();
            if (manager != null)
            {
                manager.RefreshBestiary();
            }
            
            PortalManager portalManager = FindObjectOfType<PortalManager>();
            if (portalManager != null)
            {
                portalManager.UpdatePortalVisibility();
            }
            else
            {
                Debug.Log("No PortalManager found");
            }
        }
    }

    void SetPromptAlpha(float alpha)
    {
        if (floatingPromptText != null)
        {
            Color c = floatingPromptText.color;
            c.a = alpha;
            floatingPromptText.color = c;
        }
    }
}
