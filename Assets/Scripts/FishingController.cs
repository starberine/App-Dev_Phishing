using UnityEngine;
using TMPro;

public class FishingController : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform spawnPoint;
    public TMP_Text popupText;
    private bool isInPond = false;
    private bool isFishing = false;
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>(); // Get the PlayerController on the same GameObject
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
            playerController.isFrozen = true; // Freeze movement

        ShowPopup("Fishing...");
        yield return new WaitForSeconds(2f);
        Fish();

        if (playerController != null)
            playerController.isFrozen = false; // Unfreeze after fishing
        isFishing = false;
    }

   void Fish()
{
    if (fishPrefab != null && spawnPoint != null)
    {
        GameObject fish = Instantiate(fishPrefab, spawnPoint.position, Quaternion.Euler(0, 90, 0));

        // Adjust size by setting localScale (e.g., half size)
        fish.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Destroy(fish, 2f);
        Debug.Log("Fish caught!");
        ShowPopup("Fish caught!");
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
