using System;
using System.Collections.Generic;
using UnityEngine;


public static class ColorManager
{
    private static readonly Dictionary<string, string> colorCode = new()
    {
        {"grey", "787878FF"},
        {"yellow", "FFFF00FF"},
        {"green", "00FF00FF"},
        {"blue", "0000FFFF"},
        {"white", "FFFFFFFF"}
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
    public static string GetColorCodeInShort(int page_cnt, int min_read_times) // 進捗の簡潔な表示
    {
        if (page_cnt == 0) // page_cnt: 0-> 未読; 1以上-> 途中or読了
        {
            return colorCode["grey"];
        }

        return min_read_times switch
        {
            0 => colorCode["yellow"],
            1 => colorCode["green"],
            2 => colorCode["blue"],
            _ => colorCode["white"],
        };
    }

    public static string GetColorCodeProgress(int progressNum) // 進捗の詳細な表示
    {
        return progressNum switch
        {
            0 => colorCode["grey"],
            1 => colorCode["green"],
            2 => colorCode["blue"],
            _ => colorCode["white"],// 3回以上読んだ場合は白
        };
    }
}

[Serializable]
public class SaveData
{
    public User user;
    public Character[] characters;
    public BookInfo[] book;
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
