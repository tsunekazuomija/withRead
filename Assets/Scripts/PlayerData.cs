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

    public void SetBookData(BookInfo[] book)
    {
        _saveData.book = book;
        SetData(_saveData);
    }

    public int SetExp(int exp)
    {
        int charaId = PlayerPrefs.GetInt("charaId", 1);
        int Idx = GetCharaIndexFromId(charaId);
        _saveData.characters[Idx].Exp += exp;
        _saveData.user.exp += exp;

        int levelBefore = _saveData.characters[Idx].Level;
        int uLevelBefore = _saveData.user.level;

        // Todo: level max の処理
        _saveData.characters[Idx].Level = _saveData.characters[Idx].Exp / 100 + 1 ;
        _saveData.user.level = _saveData.user.exp / 500 + 1;
        SetData(_saveData);

        if (levelBefore != _saveData.characters[Idx].Level)
        {
            return _saveData.characters[Idx].Level;
        }

        return 0;
    }

    private int GetCharaIndexFromId(int charaId)
    {
        for (int i = 0; i < _saveData.characters.Length; i++)
        {
            if (_saveData.characters[i].Id == charaId)
            {
                return i;
            }
        }
        Debug.Log("requested charaId:" + charaId + " was not found in the save data.");
        return -1;
    }

    public string GetCharaName()
    {
        int charaId = PlayerPrefs.GetInt("charaId", 1);
        int Idx = GetCharaIndexFromId(charaId);
        return _saveData.characters[Idx].Name;
    }

    public User GetUser()
    {
        return _saveData.user;
    }
}
