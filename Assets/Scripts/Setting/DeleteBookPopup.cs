using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeleteBookPopup : MonoBehaviour
{
    GameObject _canvas;
    GameObject _deleteBookPopup;
    GameObject _panel;
    SaveData _saveData;
    BookInfo[] _book;

    void Start()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = System.IO.File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);
        _book = _saveData.book;

        Button button = GetComponent<Button>();
        button.onClick.AddListener ( () => 
        {
            Clicked();   
        } );
    }

    void Clicked()
    {
        int siblingIndex = transform.GetSiblingIndex();
        _canvas = transform.parent.parent.parent.parent.gameObject;
        _deleteBookPopup = _canvas.transform.Find("DeleteBookPopup").gameObject;
        _panel = _deleteBookPopup.transform.Find("PopupPanel").gameObject;

        _panel.transform.Find("BookTitle").GetComponent<TextMeshProUGUI>().text = _book[siblingIndex].title;
        _panel.transform.Find("DeleteButton").GetComponent<Button>().onClick.AddListener ( () => 
        {
            DeleteBook(siblingIndex);
        } );
        _panel.transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener ( () => 
        {
            _deleteBookPopup.SetActive(false);
        } );
        _deleteBookPopup.SetActive(true);
    }

    void DeleteBook(int siblingIndex)
    {
        List<BookInfo> bookList = new List<BookInfo>(_book);
        bookList.RemoveAt(siblingIndex);
        _book = bookList.ToArray();
        _saveData.book = _book;
        string outputString = JsonUtility.ToJson(_saveData, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/UserData/data.json", outputString);

        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}
