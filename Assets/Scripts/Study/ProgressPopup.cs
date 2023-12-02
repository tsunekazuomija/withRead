using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ProgressPopup : MonoBehaviour
{
    GameObject _canvas;
    GameObject _progressPopup;
    GameObject _content;

    SaveData _saveData;
    BookInfo[] _book;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener ( () => 
        {
            Clicked();   
        } );

        string inputString = Resources.Load<TextAsset>("data").ToString();
        _saveData = JsonUtility.FromJson<SaveData>(inputString);
        _book = _saveData.book;
    }

    void Clicked()
    {
        int siblingIndex = transform.GetSiblingIndex();
        _canvas = transform.parent.parent.parent.parent.gameObject;
        _progressPopup = _canvas.transform.Find("ProgressPopup").gameObject;
        _content = _progressPopup.transform.Find("PopupWindow").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject;
        _content.transform.Find("TitlePlace").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = _book[siblingIndex].title;

        string progress = "";
        for (int j=0; j<_book[siblingIndex].progress.Length; ++j)
        {
            for (int k=0; k<_book[siblingIndex].progress[j].Length; ++k)
            {
                char progressChar = _book[siblingIndex].progress[j][k];
                string colorCode = GetColorCode(progressChar);
                progress += $"<color=#{colorCode}>■</color>";
            }
        }

        _content.transform.Find("ProgressPlace").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = progress;
        _progressPopup.SetActive(true);
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
