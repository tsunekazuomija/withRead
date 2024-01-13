using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class DeletePopup : MonoBehaviour
{
    public GameObject panel;

    public int bookId;

    SaveData _saveData;
    BookInfo[] _book;

    void Awake()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = System.IO.File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);
        _book = _saveData.book;
    }

    void OnEnable()
    {
        int targetIndex = 0;

        for (int i=0; i<_book.Length; ++i)
        {
            if (_book[i].id == bookId)
            {
                targetIndex = i;
                break;
            }
        }

        panel.transform.Find("BookTitle").GetComponent<TextMeshProUGUI>().text = _book[targetIndex].title;
        panel.transform.Find("DeleteButton").GetComponent<Button>().onClick.AddListener ( () => 
        {
            DeleteBook(targetIndex);
        } );
        panel.transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener ( () => 
        {
            gameObject.SetActive(false);
        } );
    }

    void DeleteBook(int targetIndex)
    {
        List<BookInfo> bookList = new List<BookInfo>(_book);
        bookList.RemoveAt(targetIndex);
        _book = bookList.ToArray();
        _saveData.book = _book;
        string outputString = JsonUtility.ToJson(_saveData);
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        System.IO.File.WriteAllText(filePath, outputString);

        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}
