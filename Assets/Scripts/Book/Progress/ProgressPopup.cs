using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// This class is assigned to the popup window.
/// This recieves the book id as a public variable from <c>PopupTrigger.cs</c>
/// and displays the progress of the book.
/// </summary>

public class ProgressPopup : MonoBehaviour
{
    private Book _book;

    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;
    [SerializeField] private GameObject bookTitle;
    [SerializeField] private GameObject bookProgress;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;

    public void Pop(Book book)
    {
        _book = book;

        bookTitle.GetComponent<TextMeshProUGUI>().text = _book.Title;
        SetSliders(slider1, slider2, _book, text1, text2);
        // display progress
        string progress = _book.ProgressString();
        bookProgress.GetComponent<TextMeshProUGUI>().richText = true; // richTextを有効にする
        bookProgress.GetComponent<TextMeshProUGUI>().text = progress;

        gameObject.SetActive(true);
    }

    private void SetSliders(Slider s1, Slider s2, Book book, TextMeshProUGUI t1, TextMeshProUGUI t2)
    {
        s1.maxValue = book.PageNum;
        s2.maxValue = book.PageNum;

        s1.onValueChanged.AddListener( (value) => t1.text = value.ToString() );
        s2.onValueChanged.AddListener( (value) => t2.text = value.ToString() );

        if (book.LastReadPage < book.PageNum)
        {
            s1.value = book.LastReadPage + 1;
            s2.value = book.LastReadPage + 1;
        }
        else
        {
            s1.value = book.PageNum;
            s2.value = book.PageNum;
        }
    }
}
