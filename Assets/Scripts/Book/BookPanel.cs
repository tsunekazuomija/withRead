using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookPanel : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject progress;

    public void SetBook(BookInfo book)
    {
        title.GetComponent<TextMeshProUGUI>().text = book.title;
        ShowProgress(book.progress_short);
    }

    public void ShowProgress(PageCell progressShort)
    {
        string progressText = "";
        for (int i=0; i < progressShort.page_cnt.Length; ++i)
        {
            string colorCode = ColorManager.GetColorCodeInShort(progressShort.page_cnt[i], progressShort.min_read_times[i]);
            progressText += $"<color=#{colorCode}>■</color>";
        }
        progress.GetComponent<TextMeshProUGUI>().text = progressText;
    }
}
