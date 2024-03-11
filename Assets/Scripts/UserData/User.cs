using UnityEngine;
using System;
using System.IO;

[Serializable]
public class User: MonoBehaviour
{
    [HideInInspector][SerializeField] private string _name;
    public string Name { get => _name; }

    [HideInInspector][SerializeField] private int _exp;
    public int Exp { get => _exp; }

    [HideInInspector][SerializeField] private int _level;
    public int Level { get => _level; }

    [SerializeField] private bool _forInit;
    private void Awake()
    {
        if (_forInit) { return; }
        Load();
    }

    public void Init(string name)
    {
        _name = name;
        _exp = 0;
        _level = 1;
        Save();
    }

    private void Save()
    {
        string filePath = Application.persistentDataPath + "/UserData/User.json";
        string jsonString = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonString);
    }

    private void Load()
    {
        void OverwriteExceptForInit(string userString)
        {
            bool forInitTmp = _forInit;
            JsonUtility.FromJsonOverwrite(userString, this);
            _forInit = forInitTmp;
        }

        string filePath = Application.persistentDataPath + "/UserData/User.json";
        string bookShelfString = File.ReadAllText(filePath);
        OverwriteExceptForInit(bookShelfString);
    }
}

