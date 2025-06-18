using UnityEngine;

public class IslandWordsManager : MonoBehaviour
{
    [System.Serializable]
    public class IslandWords
    {
        public string[] words;
    }

    [Header("Words for Each Island")]
    public IslandWords[] islandWordSets;

    public static IslandWordsManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public string GetRandomWord(int islandIndex)
    {
        if (islandIndex >= 0 && islandIndex < islandWordSets.Length)
        {
            var words = islandWordSets[islandIndex].words;
            if (words != null && words.Length > 0)
            {
                return words[Random.Range(0, words.Length)];
            }
        }

        Debug.LogWarning($"[IslandWordsManager] No words found for island index {islandIndex}!");
        return "???";
    }
}
