using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

/// <summary>
/// This class is assigned to the popup window.
/// This recieves the book id as a public variable from <c>PopupTrigger.cs</c>
/// and displays the progress of the book.
/// </summary>

public class ProgressPopup : MonoBehaviour
{
    private int bookId;
    [SerializeField] BookIdHolder bookIdHolder;

    SaveData _saveData;
    BookInfo[] _book;
    BookInfo currentBook;

    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;
    [SerializeField] private GameObject bookTitle;
    [SerializeField] private GameObject bookProgress;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;

    private void Awake()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);
        _book = _saveData.book;
    }

    private void OnEnable()
    {
        // Todo: 不正な値に対する処理(bookIdが適切でない時)
        bookId = bookIdHolder.GetId();

        for (int i=0; i<_book.Length; ++i)
        {
            if (_book[i].id == bookId)
            {
                currentBook = _book[i];
                break;
            }
        }

        bookTitle.GetComponent<TextMeshProUGUI>().text = currentBook.title;

        SetSliders(slider1, slider2, currentBook, text1, text2);

        // display progress
        string progress = "";
        for (int j=0; j<currentBook.progress.Length; ++j)
        {
            string colorCode = ColorManager.GetColorCodeProgress(currentBook.progress[j]);
            progress += $"<color=#{colorCode}>■</color>";
        }
        bookProgress.GetComponent<TextMeshProUGUI>().richText = true; // richTextを有効にする
        bookProgress.GetComponent<TextMeshProUGUI>().text = progress;
    }

    private void OnDisable()
    {
        slider1.value = currentBook.last_read + 1;
        slider2.value = currentBook.last_read + 1;
    }

    private void SetSliders(Slider s1, Slider s2, BookInfo book, TextMeshProUGUI t1, TextMeshProUGUI t2)
    {
        s1.maxValue = book.pages;
        s2.maxValue = book.pages;

        s1.onValueChanged.AddListener( (value) => t1.text = value.ToString() );
        s2.onValueChanged.AddListener( (value) => t2.text = value.ToString() );

        if (book.last_read < book.pages)
        {
            s1.value = book.last_read + 1;
            s2.value = book.last_read + 1;
        }
        else
        {
            s1.value = book.pages;
            s2.value = book.pages;
        }
    }
}
