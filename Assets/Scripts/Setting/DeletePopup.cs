using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeletePopup : MonoBehaviour
{
    private int bookId = -1;
    [SerializeField] BookIdHolder bookIdHolder;

    SaveData _saveData;
    BookInfo[] _book;

    [SerializeField] private GameObject bookTitle;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject playerData;
    [SerializeField] private GameObject editPopup;
    [SerializeField] private GameObject bookDisplay;


    void Awake()
    {
        deleteButton.GetComponent<Button>().onClick.AddListener ( () => DeleteBook() );
        cancelButton.GetComponent<Button>().onClick.AddListener ( () => gameObject.SetActive(false) );
    }

    void OnEnable()
    {
        _saveData = playerData.GetComponent<PlayerData>().GetData();
        _book = _saveData.book;

        bookId = bookIdHolder.GetId();
        int targetIndex = GetIndexFromId(bookId);
        bookTitle.GetComponent<TextMeshProUGUI>().text = _book[targetIndex].title;
    }

    void DeleteBook()
    {
        int targetIndex = GetIndexFromId(bookId);

        List<BookInfo> bookList = new(_book);
        bookList.RemoveAt(targetIndex);
        _book = bookList.ToArray();
        _saveData.book = _book;
        playerData.GetComponent<PlayerData>().SetData(_saveData);

        gameObject.SetActive(false);
        editPopup.SetActive(false);
        bookDisplay.GetComponent<ApplyBook>().Reload();
    }

    int GetIndexFromId(int id)
    {
        for (int i=0; i<_book.Length; ++i)
        {
            if (_book[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }
}
