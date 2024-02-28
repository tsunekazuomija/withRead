using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    private SaveData _saveData;
    private bool loadingDone = false;

    void Awake()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);

        loadingDone = true;
    }

    public bool IsLoadingDone()
    {
        return loadingDone;
    }

    public void ReloadData()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);
    }

    public SaveData GetData()
    {
        return _saveData;
    }

    public void SetData(SaveData data)
    {
        string outputString = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/UserData/data.json", outputString);
    }

    public User GetUser()
    {
        return _saveData.user;
    }
}
