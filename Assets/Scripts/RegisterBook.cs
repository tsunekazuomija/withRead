using UnityEngine;
using TMPro;
using System.Linq;

public class RegisterBook : MonoBehaviour
{
    TMP_InputField _inputFieldTitle;
    TMP_InputField _inputFieldNumChapter;

    public void OnclickRegisterBook()
    {
        GameObject parent = transform.parent.gameObject;
        _inputFieldTitle = parent.transform.Find("InputField-Title").GetComponent<TMP_InputField>();
        _inputFieldNumChapter = parent.transform.Find("InputField-NumChap").GetComponent<TMP_InputField>();

        string title = _inputFieldTitle.text;
        int num_chapter = int.Parse(_inputFieldNumChapter.text);

        string inputString = Resources.Load<TextAsset>("data").ToString();
        BookShelf bookShelf = JsonUtility.FromJson<BookShelf>(inputString);

        var newBook = new BookData
        {
            id = bookShelf.Book.Length,
            title = title,
            author = "",
            num_chapter = num_chapter,
            progress = Enumerable.Repeat(0, num_chapter).ToArray()
        };

        bookShelf.Book = bookShelf.Book.Concat(new BookData[] { newBook }).ToArray();
        string outputString = JsonUtility.ToJson(bookShelf, true);
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/data.json", outputString);
    }
}
