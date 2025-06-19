using UnityEngine;

public class FishPortal : MonoBehaviour
{
    [Tooltip("Fish names required to unlock this portal")]
    public string[] requiredFishNames;

    [Tooltip("The portal the player will teleport to when entering this one")]
    public Transform destinationPortal;

    [Tooltip("Island index that this portal leads to (0 = first island)")]
    public int destinationIslandIndex;

    private bool isTeleporting = false;
    private bool isOnCooldown = false;

    private void Start()
    {
        gameObject.SetActive(false); // Controlled by PortalManager
    }

    public string[] GetRequiredFish()
    {
        return requiredFishNames;
    }

    void Update()
    {
        if (!gameObject.activeSelf)
        {
            Debug.LogWarning($"[PortalA1] I was deactivated at runtime!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTeleporting || isOnCooldown) return;

        if (other.CompareTag("Player") && destinationPortal != null)
        {
            StartCoroutine(TeleportPlayer(other.transform));
        }
    }

    private System.Collections.IEnumerator TeleportPlayer(Transform player)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlayPortalTeleportSFX();

        isTeleporting = true;
        isOnCooldown = true;

        // Freeze movement
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
            playerController.isFrozen = true;

        // Teleport
        player.position = destinationPortal.position;

        // Set island index for fishing logic
        FishingController fishingController = player.GetComponent<FishingController>();
        if (fishingController != null)
            fishingController.SetIsland(destinationIslandIndex);

        yield return new WaitForSeconds(0.5f); // brief delay to unfreeze

        if (playerController != null)
            playerController.isFrozen = false;

        isTeleporting = false;

        yield return new WaitForSeconds(5f); // cooldown before portal can teleport again
        isOnCooldown = false;
    }
}
