using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [HideInInspector] public SaveData data;
    string filePath;
    string fileName = "Data.json";

    void Awake()
    {
        //filePath = Application.persistentDataPath + "/" + fileName;
        filePath = fileName;
        
        if (!File.Exists(filePath))
        {
            data = new SaveData();
            Save(data);
        }
        
        data = Load(filePath);
    }

    void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        StreamWriter wr = new StreamWriter(filePath, false);
        wr.Write(json);
        wr.Close();
    }

    SaveData Load(string path)
    {
        StreamReader rd = new StreamReader(path);
        string json = rd.ReadToEnd();
        rd.Close();

        return JsonUtility.FromJson<SaveData>(json);
    }

    void OnDestory()
    {
        Save(data);
    }
}
