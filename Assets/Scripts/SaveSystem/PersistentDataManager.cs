using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

public class PersistentDataManager 
{
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SyncDB();
#endif


    private static string path = Application.persistentDataPath + "/PhilosoMeSave.json";

    private static SaveData _current;

    public static SaveData Current => _current;

    public static bool DoesSaveFileExist()
    {
        return File.Exists(path);
    }

    public static void LoadSaveData()
    {
        if (!DoesSaveFileExist())
        {
            Debug.LogError("Tried to load a save file but it does not exist!");
            return;
        }

        string jsonText = File.ReadAllText(path);
        Debug.Log("Loaded:");
        Debug.Log(jsonText);

        _current = JsonUtility.FromJson<SaveData>(jsonText);
    }


    public static void Save()
    {
        Save(_current);
    }

    public static void Save(SaveData data)
    {
        if (data == null)
        {
            Debug.LogError("Tried to save a null SaveData!");
            return;
        }

        _current = data;
        string jsonText = JsonUtility.ToJson(data);
        Debug.Log("Saved:");
        Debug.Log(jsonText);

        File.WriteAllText(path, jsonText);

#if UNITY_WEBGL && !UNITY_EDITOR
        //flush our changes to IndexedDB
        SyncDB();
#endif
    }
}
