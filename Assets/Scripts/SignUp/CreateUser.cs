using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Runtime.CompilerServices;

[Serializable]
public class User
{
    public string name;
    public int exp;
    public int level;
    public int selectedCharaId;
}

public class CreateUser : MonoBehaviour
{
    private TMP_InputField _userName;
    [SerializeField] private GameObject inputUserName;
    [SerializeField] private SCENE mainScene;

    [SerializeField] private CharaBank refCharaBank;
    [SerializeField] private Party refParty;

    void Start()
    {
        _userName = inputUserName.GetComponent<TMP_InputField>();
    }

    public void OnClickCreateUser()
    {
        int pages = 100;

        SaveData newData = new()
        {
            user = new User
            {
                name = _userName.text,
                exp = 0,
                level = 1,
                selectedCharaId = 1
            },

            book = new BookInfo[1]
            {
                new() {
                    id = 0,
                    title = "bocchi the rock!",
                    pages = pages,
                    progress = Enumerable.Repeat(0, pages).ToArray(),
                    progress_short = new PageCell
                    {
                        page_cnt = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray(),
                        min_read_times = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray()
                    }
                }
            },
        };

        string jsonString = JsonUtility.ToJson(newData, true);
        // ディレクトリがあるか確認
        if (!Directory.Exists(Application.persistentDataPath + "/UserData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/UserData");
        }
        File.WriteAllText(Application.persistentDataPath + "/UserData/data.json", jsonString);

        CreateCharaBank();
        CreateParty();

        SceneManager.LoadScene(mainScene.ToString());
    }

    /// <summary>
    /// 初期データをまとめたクラス。
    /// </summary>
    private static class Params
    {
        public static Character[] initialChara = new Character[]
        {
            new (1, "ヨム", 0, 1, true),
            new (2, "ロボ", 0, 1, true),
            new (3, "ミズ", 0, 1, true),
            new (4, "ボス", 0, 1, true),
        };

        public static int[] initialParty = new int[] { 1, }; 
    }

    private void CreateCharaBank()
    {
        refCharaBank.Init(
            Params.initialChara
        );
    }

    private void CreateParty()
    {
        refParty.Init(
            Params.initialParty
        );
    }
}
