using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    private FishPortal[] allPortals;

    private void Start()
    {
        allPortals = FindObjectsOfType<FishPortal>(true); // Include inactive portals
        UpdatePortalVisibility();
    }

    public void UpdatePortalVisibility()
    {
        List<string> caughtFish = BestiarySaveSystem.Load();
        Debug.Log($"[PortalManager] Caught fish loaded: {string.Join(", ", caughtFish)}");

        foreach (FishPortal portal in allPortals)
        {
            string[] requiredFish = portal.GetRequiredFish();
            Debug.Log($"[PortalManager] Portal '{portal.name}' requires: {string.Join(", ", requiredFish)}");
            bool allMet = true;

            foreach (string fish in requiredFish)
            {
                if (!caughtFish.Contains(fish))
                {
                    allMet = false;
                    break;
                }
            }
            Debug.Log($"[PortalManager] Portal '{portal.name}' active: {allMet}");
            portal.gameObject.SetActive(allMet);
        }
    }
}
