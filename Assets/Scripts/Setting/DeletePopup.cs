using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class DeletePopup : MonoBehaviour
{
    private int bookId;
    [SerializeField] BookIdHolder bookIdHolder;

    SaveData _saveData;
    BookInfo[] _book;

    [SerializeField] private GameObject bookTitle;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject cancelButton;

    void Awake()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);
        _book = _saveData.book;
    }

    void OnEnable()
    {
        int targetIndex = 0;
        bookId = bookIdHolder.GetId();

        // Todo: 不正な値に対する処理(bookIdが適切でない時)
        for (int i=0; i<_book.Length; ++i)
        {
            if (_book[i].id == bookId)
            {
                targetIndex = i;
                break;
            }
        }

        bookTitle.GetComponent<TextMeshProUGUI>().text = _book[targetIndex].title;
        deleteButton.GetComponent<Button>().onClick.AddListener ( () => 
        {
            DeleteBook(targetIndex);
        } );
        cancelButton.GetComponent<Button>().onClick.AddListener ( () => 
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
        File.WriteAllText(filePath, outputString);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
