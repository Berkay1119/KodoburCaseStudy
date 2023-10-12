using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem
{
#if UNITY_EDITOR
    private readonly string _saveFolder = Application.dataPath + "/Saves/";
#else
    private readonly string SAVE_FOLDER = Application.persistentDataPath + "/Saves/";
#endif
    
    private readonly string _saveFile = "/app_data.dat";

    public SaveSystem() 
    {
        if (!Directory.Exists(_saveFolder)) 
        {
            Directory.CreateDirectory(_saveFolder);
        }
        var saveData = LoadSaveData();
        if (saveData != null) return;
        saveData = new SaveData();
        SaveTheData(saveData);
    }

    public void SaveTheData(SaveData saveData) 
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(_saveFolder + _saveFile, json);
    }

    public SaveData LoadSaveData() 
    {
        if (File.Exists(_saveFolder + _saveFile)) 
        {
            string saveText = File.ReadAllText(_saveFolder + _saveFile);
            return JsonUtility.FromJson<SaveData>(saveText);
        }
        return null;
    }

}
