using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Color teamColor;

    [System.Serializable]
    class SaveData
    {
        public Color teamColor;
    }

    public void SaveColor()
    {
        string colorPath = Application.persistentDataPath + "/savefile.json";

        SaveData data = new SaveData();
        data.teamColor = teamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(colorPath, json);
    }

    public void LoadColor()
    {
        string colorPath = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(colorPath))
        {
            string json = File.ReadAllText(colorPath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            teamColor = data.teamColor;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }
}
