using UnityEngine;

public class FishingController : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform spawnPoint;
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
