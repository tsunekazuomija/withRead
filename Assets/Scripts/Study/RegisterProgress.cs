using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class RegisterProgress : MonoBehaviour
{
    SaveData _saveData;

    int _siblingsIndex;

    // Start is called before the first frame update
    void Start()
    {
        string inputString = Resources.Load<TextAsset>("data").ToString();
        _saveData = JsonUtility.FromJson<SaveData>(inputString);

    }

    public void OnClickRegisterProgress()
    {
        string title = transform.parent.parent.Find("TitlePlace").Find("Text").GetComponent<TMPro.TextMeshProUGUI>().text;

        for (int i = 0; i < _saveData.book.Length; i++)
        {
            if (_saveData.book[i].title == title)
            {
                _siblingsIndex = i;
                break;
            }
        }

        int _startPage, _endPage;
        if (transform.parent.Find("FromPageInput").GetComponent<TMPro.TMP_InputField>().text == "")
        {
            return;
        }
        else if (transform.parent.Find("ToPageInput").GetComponent<TMPro.TMP_InputField>().text == "")
        {
            return;
        }

        _startPage = int.Parse(transform.parent.Find("FromPageInput").GetComponent<TMPro.TMP_InputField>().text);
        _endPage = int.Parse(transform.parent.Find("ToPageInput").GetComponent<TMPro.TMP_InputField>().text);
        Debug.Log(_saveData.book[_siblingsIndex].title);
        Debug.Log(_startPage);
        Debug.Log(_endPage);

        BookInfo book = _saveData.book[_siblingsIndex];
        int[] progress = book.progress;
        for (int i = _startPage - 1; i < _endPage; i++)
        {
            ++progress[i];
        }
        book.progress = progress;
        _saveData.book[_siblingsIndex] = book;

        PageCell progress_short = book.progress_short;
        int[] page_cnt = progress_short.page_cnt;
        int[] min_read_times = progress_short.min_read_times;
        for (int i = (_startPage - 1) / 10 ; i <= _endPage / 10; i++)
        {
            page_cnt[i] = 0;
            min_read_times[i] = progress[i * 10];
            for (int j = 0; j < 10; j++)
            {
                // 途中でページがなくなった場合は終了
                if (i * 10 + j >= book.pages)
                {
                    break;
                }

                if (progress[i * 10 + j] > 0)
                {
                    ++page_cnt[i];
                }
                if (progress[i * 10 + j] < min_read_times[i])
                {
                    min_read_times[i] = progress[i * 10 + j];
                }
            }
        }
        progress_short.page_cnt = page_cnt;
        progress_short.min_read_times = min_read_times;
        book.progress_short = progress_short;
        _saveData.book[_siblingsIndex] = book;

        string outputString = JsonUtility.ToJson(_saveData, true);
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/data.json", outputString);
        AssetDatabase.Refresh();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
