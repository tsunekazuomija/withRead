using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEditor;

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

[Serializable]
public class SaveData
{
    public User user;
    public Character[] characters;
    public BookInfo[] book;
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
        newData.book[0].title = "test";
        newData.book[0].pages = 100;
        newData.book[0].progress = new string[2];
        newData.book[0].progress[0] = "11111111111111100000000000000000000000000000000000";
        newData.book[0].progress[1] = "00000000000000000000000000000000000000000000000000";
        newData.book[0].progress_short = new string[1];
        newData.book[0].progress_short[0] = "1h00000000";


        string jsonString = JsonUtility.ToJson(newData, true);
        File.WriteAllText(Application.dataPath + "/Resources/data.json", jsonString);

        AssetDatabase.Refresh();
        SceneManager.LoadScene("BookShelfScene");
    }
}