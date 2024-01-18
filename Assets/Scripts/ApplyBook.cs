using System;
using System.IO;
using TMPro;
using UnityEngine;

public enum ChatColor : uint
{
    RED = 0xFF0000FF, // 赤
    GRN = 0x00FF00FF, // 緑
    BLR = 0x0000FFFF, // 青
    GRY = 0x808080FF, // 灰
    YEL = 0xFFFF00FF, // 黄
}

public static class ColorManager
{
    public static Color GetColorFromChatColor(ChatColor chatColor)
    {
        return new Color(
            (((uint) chatColor & 0xFF000000) >> 24) / 255.0f,
            (((uint) chatColor & 0x00FF0000) >> 16) / 255.0f,
            (((uint) chatColor & 0x0000FF00) >> 8) / 255.0f,
            ((uint) chatColor & 0x000000FF) / 255.0f
        );
    }

    public static string GetColorCode(int page_cnt, int min_read_times) // 進捗の簡潔な表示
    {
        if (page_cnt == 0) // page_cnt: 0-> 未読; 1以上-> 途中or読了
        {
            return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRY));
        }

        return min_read_times switch
        {
            0 => ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.YEL)),
            1 => ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRN)),
            2 => ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.BLR)),
            _ => "FFFFFF",// 全てのページを3回以上読んだ場合は白色
        };
    }

    public static string GetColorCodeProgress(int progressNum) // 進捗の詳細な表示
    {
        return progressNum switch
        {
            0 => ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRY)),
            1 => ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRN)),
            2 => ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.BLR)),
            _ => "FFFFFF",// 3回以上読んだ場合は白
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
    public int popupPurpose;
    public GameObject BookPrefab;
    
    void Start()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(inputString);
        BookInfo[] book = saveData.book;

        for (int i = 0; i < book.Length; i++)
        {
            Instantiate(BookPrefab, transform);
            GameObject bookTitle = transform.GetChild(i).GetChild(0).gameObject;  // Todo: 意図しないバグを生みかねない
            bookTitle.GetComponent<TextMeshProUGUI>().text = book[i].title;
            string progress = "";
            for (int j = 0; j < book[i].progress_short.page_cnt.Length; ++j)
            {
                string colorCode = ColorManager.GetColorCode(book[i].progress_short.page_cnt[j], book[i].progress_short.min_read_times[j]);
                progress += $"<color=#{colorCode}>■</color>";
            }
            GameObject bookProgress = transform.GetChild(i).GetChild(1).gameObject;
            bookProgress.GetComponent<TextMeshProUGUI>().richText = true; // richTextを有効にする
            bookProgress.GetComponent<TextMeshProUGUI>().text = progress;

            if (popupPurpose == 0) // register progress
            {
                transform.GetChild(i).gameObject.GetComponent<PopupTrigger>().bookId = book[i].id;
            }
            else if (popupPurpose == 1) // delete book
            {
                transform.GetChild(i).gameObject.GetComponent<DeletePopupTrigger>().bookId = book[i].id;
            }
            Debug.Log("book["+ i + "].id: " + book[i].id);
        }
    }
}
