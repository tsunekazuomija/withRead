using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;

[Serializable]
public class User
{
    public string name;
    public int exp;
}

[Serializable]
public class Character
{
    public int id;
    public string name;
    public int exp;
}

public class CreateUser : MonoBehaviour
{
    private TMP_InputField _userName;
    [SerializeField] private GameObject inputUserName;

    void Start()
    {
        _userName = inputUserName.GetComponent<TMP_InputField>();
    }

    public void OnClickCreateUser()
    {
        int pages = 35;

        SaveData newData = new()
        {
            user = new User()
        };
        newData.user.name = _userName.text;
        newData.user.exp = 0;
        newData.characters = new Character[2];
        newData.characters[0] = new Character
        {
            id = 1,
            name = "dragon",
            exp = 0
        };
        newData.characters[1] = new Character
        {
            id = 2,
            name = "kero",
            exp = 0
        };
        newData.book = new BookInfo[1];
        newData.book[0] = new BookInfo
        {
            id = 0,
            title = "bocchi the rock!",
            pages = pages,
            progress = Enumerable.Repeat(0, pages).ToArray()
        };

        PageCell pagecell = new()
        {
            page_cnt = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray(),
            min_read_times = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray()
        };
        newData.book[0].progress_short = pagecell;

        string jsonString = JsonUtility.ToJson(newData, true);
        // ディレクトリがあるか確認
        if (!Directory.Exists(Application.persistentDataPath + "/UserData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/UserData");
        }
        File.WriteAllText(Application.persistentDataPath + "/UserData/data.json", jsonString);

        SceneManager.LoadScene("BookShelfScene");
    }
}
