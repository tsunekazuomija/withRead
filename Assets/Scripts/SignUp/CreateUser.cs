using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


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

    [SerializeField] private BookShelf refBookShelf;
    [SerializeField] private CharaBank refCharaBank;
    [SerializeField] private Party refParty;

    void Start()
    {
        _userName = inputUserName.GetComponent<TMP_InputField>();
    }

    public void OnClickCreateUser()
    {
        SaveData newData = new()
        {
            user = new User
            {
                name = _userName.text,
                exp = 0,
                level = 1,
                selectedCharaId = 1
            },
        };

        string jsonString = JsonUtility.ToJson(newData, true);
        // ディレクトリがあるか確認
        if (!Directory.Exists(Application.persistentDataPath + "/UserData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/UserData");
        }
        File.WriteAllText(Application.persistentDataPath + "/UserData/data.json", jsonString);

        CreateBookShelf();
        CreateCharaBank();
        CreateParty();

        SceneManager.LoadScene(mainScene.ToString());
    }

    /// <summary>
    /// 初期データをまとめたクラス。
    /// </summary>
    private static class Params
    {
        public static Book[] initialBook = new Book[]
        {
            new (0, "bocchi the rock!", 100),
        };

        public static Character[] initialChara = new Character[]
        {
            new (1, "ヨム", 0, 1, true),
            new (2, "ロボ", 0, 1, true),
            new (3, "ミズ", 0, 1, true),
            new (4, "ボス", 0, 1, true),
        };

        public static int[] initialParty = new int[] { 1, }; 
    }


    private void CreateBookShelf()
    {
        refBookShelf.Init(
            Params.initialBook
        );
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
