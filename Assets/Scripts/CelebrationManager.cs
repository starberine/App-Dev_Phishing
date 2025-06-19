using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CelebrationManager : MonoBehaviour
{
    public GameObject celebrationPanel;
    public ParticleSystem celebrationParticles;
    public AudioSource celebrationSound;

    private CanvasGroup canvasGroup;
    private CelebrationSaveData celebrationData;

    void Awake()
    {
        if (celebrationPanel != null)
            canvasGroup = celebrationPanel.GetComponent<CanvasGroup>();

        celebrationData = CelebrationSaveSystem.Load(); // Load the flag
    }

    public void CheckCompletion(List<FishData> allFish, HashSet<string> caughtFishNames)
    {
        if (celebrationData == null || celebrationData.hasCelebrated)
            return;

        bool isComplete = allFish.TrueForAll(fish => caughtFishNames.Contains(fish.fishName));

        if (isComplete)
        {
            TriggerCelebration();
            celebrationData.hasCelebrated = true;
            CelebrationSaveSystem.Save(celebrationData); // Save the flag
        }
    }

    private void TriggerCelebration()
    {
        Debug.Log("[Celebration] Bestiary completed!");

        if (celebrationPanel != null)
        {
            celebrationPanel.SetActive(true);
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                StartCoroutine(DelayedFadeOut());
            }
        }

        if (celebrationParticles != null)
            celebrationParticles.Play();

        if (celebrationSound != null)
            celebrationSound.Play();
    }

    private IEnumerator DelayedFadeOut()
    {
        yield return new WaitForSeconds(5f); // Wait before fading

        float duration = 3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = 1f - (elapsed / duration);
            canvasGroup.alpha = t;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        celebrationPanel.SetActive(false);
    }
}
