using UnityEngine;
using System.IO;

public static class CelebrationSaveSystem
{
    private static string filePath => Path.Combine(Application.persistentDataPath, "celebration.json");

    public static void Save(CelebrationSaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public static CelebrationSaveData Load()
    {
        if (!File.Exists(filePath))
            return new CelebrationSaveData(); // Return fresh data if file not found

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<CelebrationSaveData>(json);
    }

    public static void ResetCelebrationFlag()
    {
        Save(new CelebrationSaveData { hasCelebrated = false });
        Debug.Log("[CelebrationSaveSystem] hasCelebrated flag reset.");
    }
}
