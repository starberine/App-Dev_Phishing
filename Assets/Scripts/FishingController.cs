using UnityEngine;
using TMPro;

public class FishingController : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform spawnPoint;
    public TMP_Text popupText;
    public FishData[] availableFish;
    private bool isInPond = false;
    private bool isFishing = false;
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isInPond && !isFishing && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FishingRoutine());
        }
    }

    System.Collections.IEnumerator FishingRoutine()
    {
        isFishing = true;
        if (playerController != null)
            playerController.isFrozen = true; 

        ShowPopup("fishing...");
        yield return new WaitForSeconds(3f);
        Fish();

        if (playerController != null)
            playerController.isFrozen = false; 
        isFishing = false;
    }

    void Fish()
    {
        if (availableFish.Length == 0 || spawnPoint == null)
            return;

        FishData selectedFish = availableFish[Random.Range(0, availableFish.Length)];

        if (selectedFish.fishModel != null)
        {
            GameObject fish = Instantiate(selectedFish.fishModel, spawnPoint.position, Quaternion.Euler(0, 90, 0));
            fish.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // optional scale
            Destroy(fish, 2f);

            Debug.Log("caught: " + selectedFish.fishName);
            ShowPopup("caught: " + selectedFish.fishName);

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayFishingCaughtSFX();
            }
        }
        else
        {
            Debug.LogWarning("No model assigned for " + selectedFish.fishName);
        }
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
}
