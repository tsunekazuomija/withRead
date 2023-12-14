using System;
using TMPro;
using UnityEngine;
using System.IO;

public enum ChatColor : uint
{
    RED = 0xFF0000FF, // 赤
    GRN = 0x00FF00FF, // 緑
    BLR = 0x0000FFFF, // 青
    GRY = 0x808080FF, // 灰
    YEL = 0xFFFF00FF, // 黄
}

public class ApplyBook : MonoBehaviour
{
    public GameObject BookPrefab;

    void Start()
    {
        string dataPath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(dataPath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(inputString);
        BookInfo[] book = saveData.book;
        for (int i = 0; i < book.Length; i++)
        {
            Instantiate(BookPrefab, transform);
            GameObject bookTitle = transform.GetChild(i).GetChild(0).gameObject;
            bookTitle.GetComponent<TextMeshProUGUI>().text = book[i].title;
            string progress = "";
            for (int j = 0; j < book[i].progress_short.page_cnt.Length; ++j)
            {
                string colorCode = GetColorCode(book[i].progress_short.page_cnt[j], book[i].progress_short.min_read_times[j]);
                progress += $"<color=#{colorCode}>■</color>";
            }
            GameObject bookProgress = transform.GetChild(i).GetChild(1).gameObject;
            bookProgress.GetComponent<TextMeshProUGUI>().richText = true; // richTextを有効にする
            bookProgress.GetComponent<TextMeshProUGUI>().text = progress;
        }
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

    string GetColorCode(int page_cnt, int min_read_times)
    {
        if (page_cnt == 0) // page_cnt: 0-> 未読; 1以上-> 途中or読了
        {
            return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRY));
        }
        
        switch (min_read_times)
        {
            case 0:
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.YEL));
            case 1:
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRN));
            case 2:
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.BLR));
            default:
                return "FFFFFF"; // 全てのページを3回以上読んだ場合は白色
        }
    }
}
