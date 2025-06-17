using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class BestiarySaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/bestiary.json";

    [System.Serializable]
    private class SaveData
    {
        public List<string> caughtFishNames = new List<string>();
    }

    public static void Save(List<string> caughtFish)
    {
        SaveData data = new SaveData { caughtFishNames = caughtFish };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static List<string> Load()
    {
        if (!File.Exists(SavePath))
            return new List<string>();

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data?.caughtFishNames ?? new List<string>();
    }

    public static void Clear()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Bestiary save data cleared.");
        }
    }
}
