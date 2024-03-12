using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

/// <summary>
/// キャラクターのデータを保持するクラス
/// </summary>
[Serializable]
public class CharaBank : MonoBehaviour
{
    private Dictionary<int, Character> _characterDict;
    public Dictionary<int, Character> Characters
    {
        get { return _characterDict; }
    }


    /// <summary>
    /// Since Dictionary<int, Character> cannot be saved by JsonUtility,
    /// use an array instead.
    /// Do not use this field outside of method Save() and Load().
    /// </summary>
    [SerializeField][HideInInspector] private Character[] characterArray;

    private bool _loadingDone = false;
    public bool LoadingDone
    {
        get { return _loadingDone; }
    }

    // true for initialization of this class
    [SerializeField] private bool forInit;
    private void Awake()
    {
        if (forInit)
        {
            return;
        }
        Load();

        _loadingDone = true;
    }

    /// <summary>
    /// キャラクターのデータを初期化する
    /// </summary>
    /// <param name="characters"></param>
    /// <returns></returns>
    public void Init(Character[] characters)
    {
        bool DirectoryExists()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/UserData"))
            {
                Debug.Log(Application.persistentDataPath + "/UserData" + " was not found.");
                return false;
            }
            return true;
        }

        bool FileExists()
        {
            if (File.Exists(Application.persistentDataPath + "/UserData/CharaBank.json"))
            {
                Debug.Log(Application.persistentDataPath + "/UserData/CharaBank.json" + " already exists.");
                return true;
            }
            return false;
        }

        // main process
        if (!DirectoryExists() || FileExists())
        {
            return;
        }

        _characterDict = new Dictionary<int, Character>();
        foreach (var character in characters)
        {
            _characterDict.Add(character.Id, character);
        }
        Save();
        return;
    }

    /// <summary>
    /// クラスのデータをJsonファイルに保存する。
    /// CharacterDictをCharacterArrayに上書きしてから保存する。
    /// </summary>
    private void Save()
    {
        void SyncToCharacterArray()
        {
            characterArray = new List<Character>(_characterDict.Values).ToArray();
        }

        SyncToCharacterArray();
        string outputString = JsonUtility.ToJson(this);
        File.WriteAllText(Application.persistentDataPath + "/UserData/CharaBank.json", outputString);
    }

    /// <summary>
    /// キャラクターのデータをCharacterDictにロードする
    /// </summary>
    private void Load()
    {
        void LoadToCharacterDict()
        {
            _characterDict = new Dictionary<int, Character>();
            foreach (var character in characterArray)
            {
                _characterDict.Add(character.Id, character);
            }
        }

        /// <summary>
        /// forInitフィールドを無視してJsonファイルからデータをロードする。
        /// </summary>
        /// <param name="charaBankString"></param>
        void OverwriteExceptForInit(string charaBankString)
        {
            bool forInitTmp = forInit;
            JsonUtility.FromJsonOverwrite(charaBankString, this);
            forInit = forInitTmp;
        }

        string filePath = Application.persistentDataPath + "/UserData/CharaBank.json";
        string charaBankString = File.ReadAllText(filePath);
        OverwriteExceptForInit(charaBankString);
        LoadToCharacterDict();
    }

    public void GainMagicPoint(int charaId, int mp)
    {
        if (mp <= 0)
            return;
        
        _characterDict[charaId].GainMagicPoint(mp);
        Save();
    }

    public void SpendAllMagicPoint(int charaId)
    {
        _characterDict[charaId].SpendAllMagicPoint();
        Save();
    }

    public void GainExp(int charaId, int exp)
    {
        if (exp <= 0)
            return;

        _characterDict[charaId].GainExp(exp);
        Save();
    }

    private static class Calc
    {
        public static int GetMPFromPage(int pageNum)
        {
            return pageNum * 5;
        }
    }
}

