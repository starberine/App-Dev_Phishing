using UnityEngine;
using TMPro; // Use TextMeshPro

public class FishingController : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform spawnPoint;
    public TMP_Text popupText; // TMP instead of regular Text
    private bool isInPond = false;

    void Update()
    {
        if (isInPond && Input.GetKeyDown(KeyCode.E))
        {
            Fish();
        }
    }

    void Fish()
    {
        if (fishPrefab != null && spawnPoint != null)
        {
            GameObject fish = Instantiate(fishPrefab, spawnPoint.position, Quaternion.identity);
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
