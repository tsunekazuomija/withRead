using System;
using TMPro;
using UnityEngine;



public class ApplyBookStudy : MonoBehaviour
{
    public GameObject BookPrefab;
    
    void Start()
    {
        string inputString = Resources.Load<TextAsset>("data").ToString();

        SaveData saveData = JsonUtility.FromJson<SaveData>(inputString);
        BookInfo[] book = saveData.book;
        for (int i = 0; i < book.Length; i++)
        {
            Instantiate(BookPrefab, transform);
            GameObject bookTitle = transform.GetChild(i).GetChild(0).gameObject;
            bookTitle.GetComponent<TextMeshProUGUI>().text = book[i].title;
            string progress = "";
            for (int j = 0; j < book[i].progress_short.Length; ++j)
            {
                for (int k = 0; k < book[i].progress_short[j].Length; ++k)
                {
                    char progressChar = book[i].progress_short[j][k];
                    string colorCode = GetColorCode(progressChar);
                    progress += $"<color=#{colorCode}>■</color>";
                }
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
    

    string GetColorCode(char progressChar)
    {
        switch (progressChar)
        {
            case '0':
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRY));
            case 'h':
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.YEL));
            case '1':
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.GRN));
            case '2':
                return ColorUtility.ToHtmlStringRGBA(GetColorFromChatColor(ChatColor.BLR));
            default:
                return "FFFFFF"; // デフォルトは白色
        }
    }
}
