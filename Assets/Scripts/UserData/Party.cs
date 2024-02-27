using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

/// <summary>
/// パーティのデータを保持するクラス。パーティに所属するキャラクターのIDのみ保持すれば十分かも。
/// であれば、今のところ、このクラスは不要かも。
/// </summary>
[Serializable]
public class Party : MonoBehaviour
{
    public static class Params
    {
        public static int MaxMember = 4;
    }

    /// <summary>
    /// パーティに所属するキャラクターのID
    /// </summary>
    [SerializeField][HideInInspector] private int[] partyMemberIndex;
    public int[] PartyMemberIndex
    {
        get { return partyMemberIndex; }
        set { partyMemberIndex = value; }
    }

    [SerializeField] Button saveButton;

    [SerializeField] private bool forInit;
    private void Awake()
    {
        if (forInit)
        {
            return;
        }

        saveButton.onClick.AddListener(() =>
        {
            Save();
        });
        Load();
    }

    public void Init(int[] initialMemberIndex)
    {
        static bool DirectoryExists()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/UserData"))
            {
                Debug.Log(Application.persistentDataPath + "/UserData" + " was not found.");
                return false;
            }
            return true;
        }

        if (!DirectoryExists())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/UserData");
        }

        partyMemberIndex = initialMemberIndex;
        Save();
    }

    private void Save()
    {
        string filePath = Application.persistentDataPath + "/UserData/Party.json";
        string partyString = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, partyString);
    }

    private void Load()
    {
        string filePath = Application.persistentDataPath + "/UserData/Party.json";
        string partyString = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(partyString, this);
    }
}
