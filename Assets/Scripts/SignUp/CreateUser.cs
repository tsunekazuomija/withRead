using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEditor;
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

        SaveData newData = new SaveData();
        newData.user = new User();
        newData.user.name = _userName.text;
        newData.user.exp = 0;
        newData.characters = new Character[1];
        newData.characters[0] = new Character();
        newData.characters[0].id = 0;
        newData.characters[0].name = "kita";
        newData.characters[0].exp = 0;
        newData.book = new BookInfo[1];
        newData.book[0] = new BookInfo();
        newData.book[0].id = 0;
        newData.book[0].title = "bocchi the rock!";
        newData.book[0].pages = pages;
        newData.book[0].progress = Enumerable.Repeat(0, pages).ToArray();
        
        PageCell pagecell = new PageCell();
        pagecell.page_cnt = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray();
        pagecell.min_read_times = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray();
        newData.book[0].progress_short = pagecell;

        string jsonString = JsonUtility.ToJson(newData, true);
        File.WriteAllText(Application.dataPath + "/Resources/data.json", jsonString);

        AssetDatabase.Refresh();
        SceneManager.LoadScene("BookShelfScene");
    }
}
