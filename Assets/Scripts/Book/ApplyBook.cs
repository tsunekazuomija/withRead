using System;
using System.Collections.Generic;
using UnityEngine;


public static class ColorManager
{
    private static readonly Dictionary<string, string> colorCode = new()
    {
        {"grey", "808080FF"},
        {"blue1", "DCFBFFFF"},
        {"blue2", "3399FFFF"},
        {"blue3", "0066CCFF"},
        {"blue4", "004C99FF"}
    };

    /// <summary>
    /// get color to display book progress in short
    /// </summary>
    /// <param name="page_cnt">
    /// number of pages being read (0: not read; 1 or more (<= 10): in progress or read)
    /// </param>
    /// <param name="min_read_times">
    /// The number of times the least read page has been read among the target pages.
    /// </param>
    /// <returns>
    /// color code
    /// </returns>
    public static string GetColorCodeInShort(int page_cnt, int min_read_times)
    {
        if (page_cnt == 0)
        {
            return colorCode["grey"];
        }

        return min_read_times switch
        {
            0 => colorCode["blue1"],
            1 => colorCode["blue2"],
            2 => colorCode["blue3"],
            _ => colorCode["blue4"],
        };
    }

    /// <summary>
    /// get color to display book progress page by page
    /// </summary>
    /// <param name="progressNum">
    /// The number of times the page has been read.
    /// </param>
    /// <returns>
    /// color code
    /// </returns>
    public static string GetColorCodeProgress(int progressNum)
    {
        return progressNum switch
        {
            0 => colorCode["grey"],
            1 => colorCode["blue2"],
            2 => colorCode["blue3"],
            _ => colorCode["blue4"],
        };
    }
}

[Serializable]
public class BookInfo
{
    public int id;
    public string title;
    public int pages;
    public int[] progress;
    public PageCell progress_short;
    public int last_read;
}

[Serializable]
public class PageCell
{
    public int[] page_cnt;
    public int[] min_read_times;
}

public class ApplyBook : MonoBehaviour
{
    [SerializeField] private GameObject BookPrefab;
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject playerData;
    
    void Start()
    {
        SaveData saveData = playerData.GetComponent<PlayerData>().GetData();
        BookInfo[] book = saveData.book;

        for (int i = 0; i < book.Length; i++)
        {
            var bookPanel = Instantiate(BookPrefab, transform);

            bookPanel.GetComponent<BookPanel>().SetBook(book[i]);
            bookPanel.GetComponent<PopupTrigger>().SetId(book[i].id);
            bookPanel.GetComponent<PopupTrigger>().SetPopup(popup);
        }
    }

    public void Reload()
    {
        SaveData saveData = playerData.GetComponent<PlayerData>().GetData();
        BookInfo[] book = saveData.book;

        foreach (Transform n in gameObject.transform)
        {
            Destroy(n.gameObject);
        }

        for (int i = 0; i < book.Length; i++)
        {
            var bookPanel = Instantiate(BookPrefab, transform);

            bookPanel.GetComponent<BookPanel>().SetBook(book[i]);
            bookPanel.GetComponent<PopupTrigger>().SetId(book[i].id);
            bookPanel.GetComponent<PopupTrigger>().SetPopup(popup);
        }
    }
}
