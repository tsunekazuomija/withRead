using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

/// <summary>
/// This class is assigned to the popup window.
/// This recieves the book id as a public variable from 
/// and displays the progress of the book.
/// </summary>


public class ProgressPopup : MonoBehaviour
{
    public GameObject content;
    public int bookId;

    SaveData _saveData;
    BookInfo[] _book;
    BookInfo currentBook;

    Slider slider1;
    Slider slider2;

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

        content.transform.Find("TitlePlace").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = currentBook.title;

        // set maxvalue of slider
        slider1 = content.transform.Find("RegisterPlace/Slider-from").GetComponent<Slider>();
        slider2 = content.transform.Find("RegisterPlace/Slider-to").GetComponent<Slider>();
        slider1.maxValue = currentBook.pages;
        slider2.maxValue = currentBook.pages;

        GameObject textBox1 = content.transform.Find("RegisterPlace/Text-box-from").gameObject;
        GameObject textBox2 = content.transform.Find("RegisterPlace/Text-box-to").gameObject;

        slider1.onValueChanged.AddListener( (value) => 
        {
            textBox1.GetComponent<TextMeshProUGUI>().text = value.ToString();
        } );
        slider2.onValueChanged.AddListener( (value) => 
        {
            textBox2.GetComponent<TextMeshProUGUI>().text = value.ToString();
        } );

        // display progress
        string progress = "";
        for (int j=0; j<currentBook.progress.Length; ++j)
        {
            string colorCode = GetColorCodeProgress(currentBook.progress[j]);
            progress += $"<color=#{colorCode}>■</color>";
        }
        GameObject bookProgress = content.transform.Find("ProgressPlace/Text").gameObject;
        bookProgress.GetComponent<TextMeshProUGUI>().richText = true; // richTextを有効にする
        bookProgress.GetComponent<TextMeshProUGUI>().text = progress;
    }

    void OnDisable()
    {
        slider1.value = 1;
        slider2.value = 1;
    }

    Color GetColorFromChatColor(ChatColor chatColor)
    {
        return new Color(
            (((uint) chatColor & 0xFF000000) >> 24) / 255.0f,
            (((uint) chatColor & 0x00FF0000) >> 16) / 255.0f,
            (((uint) chatColor & 0x0000FF00) >> 8) / 255.0f,
            ((uint) chatColor & 0x000000FF) / 255.0f
        );
    }

    string GetColorCodeProgress(int progressNum)
    {
        switch (progressNum)
        {
            case 0:
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRY));
            case 1:
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRN));
            case 2:
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.BLR));
            default:
                return "FFFFFF"; // 3回以上読んだ場合は白
        }
    }
}
