using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CelebrationManager : MonoBehaviour
{
    public GameObject celebrationPanel;
    public ParticleSystem celebrationParticles;
    public AudioSource celebrationSound;

    private CanvasGroup canvasGroup; // ✅ for fading
    private bool hasCelebrated = false;

    void Awake()
    {
        if (celebrationPanel != null)
            canvasGroup = celebrationPanel.GetComponent<CanvasGroup>();
    }

    public void CheckCompletion(List<FishData> allFish, HashSet<string> caughtFishNames)
    {
        if (hasCelebrated) return;

        bool isComplete = allFish.TrueForAll(fish => caughtFishNames.Contains(fish.fishName));

        if (isComplete)
        {
            TriggerCelebration();
            hasCelebrated = true;
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
                StartCoroutine(FadeOutCelebration());
            }
        }

        if (celebrationParticles != null)
            celebrationParticles.Play();

        if (celebrationSound != null)
        {
            Debug.Log("[Celebration] Playing celebration sound!");
            celebrationSound.Play();
        }
        else
        {
            Debug.LogWarning("[Celebration] celebrationSound is null!");
        }
    }

    private IEnumerator FadeOutCelebration()
    {
        float holdDuration = 4f;
        float fadeDuration = 2f;

        // Wait while panel is fully visible
        yield return new WaitForSeconds(holdDuration);

        // Then fade out
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = 1f - (elapsed / fadeDuration);
            canvasGroup.alpha = t;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        celebrationPanel.SetActive(false);
    }

}
