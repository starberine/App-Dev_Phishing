using System.Collections.Generic;
using UnityEngine;

public class BestiaryManager : MonoBehaviour
{
    public static BestiaryManager Instance;

    private HashSet<string> caughtFish = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterFish(string fishName)
    {
        if (caughtFish.Add(fishName))
        {
            Debug.Log($"New fish caught: {fishName}!");
        }
        else
        {
            Debug.Log($"Caught another {fishName}.");
        }
    }

    public IEnumerable<string> GetCaughtFish()
    {
        return caughtFish;
    }
}
