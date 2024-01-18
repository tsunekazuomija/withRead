using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// This class is assigned to popup window.
/// </summary>

public class RegisterBook : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField pages;
    public Button registerButton;

    SaveData _saveData;

    void Awake()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);

        registerButton.onClick.AddListener( () => 
        {
            Register();
        } );
    }

    void Register()
    {
        string titleName = title.text;
        int numPage = int.Parse(pages.text);

        int[] progress = Enumerable.Repeat(0, numPage).ToArray();
        PageCell progress_short = new()
        {
            page_cnt = Enumerable.Repeat(0, Mathf.CeilToInt(numPage / 10f)).ToArray(),
            min_read_times = Enumerable.Repeat(0, Mathf.CeilToInt(numPage / 10f)).ToArray()
        };

        int id;
        if (_saveData.book.Length == 0)
        {
            id = 0;
        }
        else
        {
            id = _saveData.book.Last().id + 1;
        }

        BookInfo newBook = new()
        {
            id = id,
            title = titleName,
            pages = numPage,
            progress = progress,
            progress_short = progress_short,
            last_read = 0
        };
        
        _saveData.book = _saveData.book.Concat(new BookInfo[] { newBook }).ToArray();
        string outputString = JsonUtility.ToJson(_saveData, true);
        File.WriteAllText(Application.persistentDataPath + "/UserData/data.json", outputString);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
