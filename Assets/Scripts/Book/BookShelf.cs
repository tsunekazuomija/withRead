using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

/// <summary>
/// A class to manage data of books.
/// </summary>
[Serializable]
public class BookShelf : MonoBehaviour
{
    private Dictionary<int, Book> _bookDict;
    public Dictionary<int, Book> BookDict
    {
        get { return _bookDict; }
    }

    private int _lastId;

    /// <summary>
    /// Since Dictionary<int, Book> cannot be saved by JsonUtility,
    /// use an array instead.
    /// Do not use this field outside of method Save() and Load().
    /// </summary>
    [SerializeField][HideInInspector] private Book[] bookArray;

    // true for initialization of this class
    [SerializeField] private bool forInit;
    private void Awake()
    {
        if (forInit)
        {
            return;
        }
        Load();
    }

    public void Init(Book[] books)
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
            Debug.Log("Directory not found");
            return;
        }

        _bookDict = new Dictionary<int, Book>();
        foreach (var book in books)
        {
            _bookDict.Add(book.Id, book);
        }

        _lastId = _bookDict.Keys.Max();
        Save();
    }

    private void Save()
    {
        void SyncToBookArray()
        {
            bookArray = new List<Book>(_bookDict.Values).ToArray();
        }

        string filePath = Application.persistentDataPath + "/UserData/BookShelf.json";
        SyncToBookArray();
        string bookShelfString = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, bookShelfString);
    }

    private void Load()
    {
        void LoadToBookDict()
        {
            _bookDict = new Dictionary<int, Book>();
            foreach (var book in bookArray)
            {
                _bookDict.Add(book.Id, book);
            }
        }

        void OverwriteExceptForInit(string bookShelfString)
        {
            bool forInitTmp = forInit;
            JsonUtility.FromJsonOverwrite(bookShelfString, this);
            forInit = forInitTmp;
        }

        string filePath = Application.persistentDataPath + "/UserData/BookShelf.json";
        string bookShelfString = File.ReadAllText(filePath);
        OverwriteExceptForInit(bookShelfString);
        LoadToBookDict();
    }

    public void RegisterProgress(int bookId, int startPage, int endPage)
    {
        Book book = _bookDict[bookId];
        book.SetProgress(startPage, endPage);
        Save();
    }

    public void DeleteBook(int bookId)
    {
        _bookDict.Remove(bookId);
        Save();
    }

    public void AddBook(string title, int pageNum)
    {
        _bookDict.Add(++_lastId, new Book(_lastId, title, pageNum));
        Save();
    }

    public void UpdateBook(int id, string title, int pageNum)
    {
        _bookDict[id].UpdateInfo(title, pageNum);
        Save();
    }
}
