using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// This class is assigned to popup window.
/// </summary>

public class RegisterBook : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField pages;
    public Button registerButton;
    [SerializeField] private GameObject playerData;
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject bookList;

    SaveData _saveData;

    void Start()
    {
        _saveData = playerData.GetComponent<PlayerData>().GetData();
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
        playerData.GetComponent<PlayerData>().SetData(_saveData);

        popup.SetActive(false);
        title.text = "";
        pages.text = "";

        bookList.GetComponent<ApplyBook>().Reload();
    }
}
