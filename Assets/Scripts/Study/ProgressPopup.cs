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
    public int bookId;

    SaveData _saveData;
    BookInfo[] _book;
    BookInfo currentBook;

    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;
    [SerializeField] private GameObject bookTitle;
    [SerializeField] private GameObject bookProgress;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;

    void Awake()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);
        _book = _saveData.book;
    }

    void OnEnable()
    {
        // Todo: 不正な値に対する処理(bookIdが適切でない時)

        for (int i=0; i<_book.Length; ++i)
        {
            if (_book[i].id == bookId)
            {
                currentBook = _book[i];
                break;
            }
        }

        bookTitle.GetComponent<TextMeshProUGUI>().text = currentBook.title;

        // set maxvalue of slider
        slider1.maxValue = currentBook.pages;
        slider2.maxValue = currentBook.pages;

        slider1.onValueChanged.AddListener( (value) => 
        {
            text1.text = value.ToString();
        } );
        slider2.onValueChanged.AddListener( (value) => 
        {
            text2.text = value.ToString();
        } );

        if (currentBook.last_read < currentBook.pages)
        {
            slider1.value = currentBook.last_read + 1;
            slider2.value = currentBook.last_read + 1;
        }
        else
        {
            slider1.value = currentBook.pages;
            slider2.value = currentBook.pages;
        }

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

    void OnDisable()
    {
        slider1.value = currentBook.last_read + 1;
        slider2.value = currentBook.last_read + 1;
    }
}
