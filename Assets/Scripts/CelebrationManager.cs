using UnityEngine;
using System.Collections.Generic; // Needed for List<> and HashSet<>


public class CelebrationManager : MonoBehaviour
{
    public GameObject celebrationPanel;        // Optional UI message
    public ParticleSystem celebrationParticles; // Confetti or fireworks
    public AudioSource celebrationSound;       // Cheer sound or fanfare

    private bool hasCelebrated = false;

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
            celebrationPanel.SetActive(true);

        if (celebrationParticles != null)
            celebrationParticles.Play();

        if (celebrationSound != null)
            celebrationSound.Play();
    }
}
