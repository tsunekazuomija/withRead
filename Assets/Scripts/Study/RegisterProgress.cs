using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// progresspopupにまとめてしまうこともできるが、あまりにも長くなりそうなので分離する。
/// registerボタンに割り当てる。
/// </summary>

public class RegisterProgress : MonoBehaviour
{
    public Slider slider1; // from
    public Slider slider2; // to

    SaveData _saveData;
    BookInfo[] _book;

    [SerializeField] GameObject popup;

    void Awake()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);
        _saveData = JsonUtility.FromJson<SaveData>(inputString);
        _book = _saveData.book;
    }

    public void OnClickRegisterProgress()
    {
        int _startPage = (int) slider1.value;
        int _endPage = (int) slider2.value;
        if (_endPage < _startPage)
        {
            Debug.Log("start page num is larger than end page num");
            return;
        }

        int _bookId = popup.GetComponent<BookIdHolder>().GetId();

        int targetIndex = 0;
        for (int i=0; i < _book.Length; ++i)
        {
            if (_book[i].id == _bookId)
            {
                targetIndex = i;
                break;
            }
        }

        // Todo: targetIndexが見つからなかった場合の処理

        BookInfo targetBook = _book[targetIndex];

        targetBook.progress = GetProgress(targetBook.progress, _startPage, _endPage);
        targetBook.progress_short = GetProgressShort(targetBook.progress_short, targetBook.progress, targetBook.pages, _startPage, _endPage);
        targetBook.last_read = _endPage;

        _book[targetIndex] = targetBook;
        _saveData.book = _book;

        string outputString = JsonUtility.ToJson(_saveData, true);
        File.WriteAllText(Application.persistentDataPath + "/UserData/data.json", outputString);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    int[] GetProgress(int[] progress, int _startPage, int _endPage)
    {
        for (int i = _startPage - 1; i < _endPage; i++)
        {
            ++progress[i];
        }
        return progress;
    }

    PageCell GetProgressShort(PageCell progress_s, int[] progress, int pagenum, int startPage, int endPage)
    {
        int[] page_cnt = progress_s.page_cnt;
        int[] min_read_times = progress_s.min_read_times;

        int startIdx = startPage - 1;
        int endIdx = endPage - 1;

        for (int i = (startIdx) / 10; i < endIdx / 10 + 1; ++i)
        {
            page_cnt[i] = 0;
            min_read_times[i] = 0;
            for (int j = 0; j < 10; ++j)
            {
                // finish if exceed maximum page
                if (i * 10 + j > pagenum - 1)
                {
                    break;
                }

                // count studied page
                if (progress[i * 10 + j] > 0)
                {
                    ++page_cnt[i];
                }

                // get minimum read times
                if (j == 0)
                {
                    min_read_times[i] = progress[i * 10 + j]; // = progress[i * 10]
                } 
                else if (progress[i * 10 + j] < min_read_times[i])
                {
                    min_read_times[i] = progress[i * 10 + j];
                }
            }
        }
        progress_s.page_cnt = page_cnt;
        progress_s.min_read_times = min_read_times;
        return progress_s;
    }
}

