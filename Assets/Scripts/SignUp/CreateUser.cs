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
    TMP_InputField _userName;

    GameObject _canvas;

    // Start is called before the first frame update
    void Start()
    {
        //_parent = transform.parent.gameObject;
        _canvas = transform.parent.parent.parent.gameObject;
        _userName = _canvas.transform.Find("NameInputField").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void OnClickCreateUser()
    {
        int pages = 35;

        SaveData newData = new SaveData
        {
            user = new User()
        };
        newData.user.name = _userName.text;
        newData.user.exp = 0;
        newData.characters = new Character[1];
        newData.characters[0] = new Character
        {
            id = 0,
            name = "kita",
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
