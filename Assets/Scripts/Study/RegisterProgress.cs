using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// progresspopupにまとめてしまうこともできるが、あまりにも長くなりそうなので分離する。
/// registerボタンに割り当てる。
/// </summary>

public class RegisterProgress : MonoBehaviour
{
    public Slider slider1; // from
    public Slider slider2; // to

    private BookInfo[] _book;

    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject bookDisplay;
    [SerializeField] private GameObject playerData;
    [SerializeField] private GameObject messageBoard;

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

        // register book progress
        _book = playerData.GetComponent<PlayerData>().GetData().book;
        int targetIndex = GetBookIndex(_bookId);
        BookInfo targetBook = _book[targetIndex];
        targetBook.progress = GetProgress(targetBook.progress, _startPage, _endPage);
        targetBook.progress_short = GetProgressShort(targetBook.progress_short, targetBook.progress, targetBook.pages, _startPage, _endPage);
        targetBook.last_read = _endPage;
        _book[targetIndex] = targetBook;
        playerData.GetComponent<PlayerData>().SetBookData(_book);

        // register experience point
        int readPage = _endPage - _startPage + 1;
        int exp = readPage * 10;
        // string charaName = playerData.GetComponent<PlayerData>().GetCharaName();
        // int level = playerData.GetComponent<PlayerData>().SetExp(exp);
        string message = targetBook.title + " を " + readPage + "ページ よんだ。\n";
        // message += charaName + " の かしこさが " + exp + " あがった。\n";

        // if (level > 0)
        // {
        //     message += "レベルが " + level + " になった。";
        // }

        messageBoard.GetComponent<Console>().SetMessage(message);


        popup.GetComponent<ProgressPopup>().ReloadSaveData();
        popup.SetActive(false);
        bookDisplay.GetComponent<ApplyBook>().Reload();
    }

    private int[] GetProgress(int[] progress, int _startPage, int _endPage)
    {
        for (int i = _startPage - 1; i < _endPage; i++)
        {
            ++progress[i];
        }
        return progress;
    }

    private PageCell GetProgressShort(PageCell progress_s, int[] progress, int pagenum, int startPage, int endPage)
    {
        int[] page_cnt = progress_s.page_cnt;
        int[] min_read_times = progress_s.min_read_times;

        int startIdx = startPage - 1;
        int endIdx = endPage - 1;

        for (int i = startIdx / 10; i < endIdx / 10 + 1; ++i)
        {
            page_cnt[i] = 0;
            min_read_times[i] = 0;
            for (int j = 0; j < 10; ++j)
            {
                
                if (i * 10 + j > pagenum - 1)  // finish if exceed maximum page
                {
                    break;
                }

                if (progress[i * 10 + j] > 0)  // count studied page
                {
                    ++page_cnt[i];
                }

                if (j == 0)  // get minimum read times
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

    private int GetBookIndex(int _bookId)
    {
        int targetIndex = 0;
        for (int i = 0; i < _book.Length; ++i)
        {
            if (_book[i].id == _bookId)
            {
                targetIndex = i;
                break;
            }
        }
        return targetIndex;
    }
}

