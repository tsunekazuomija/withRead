using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


[Serializable]
public class Book
{
    [Serializable]
    public class ProgressInShort
    {
        [SerializeField] private int[] _pageCountArray;
        public int[] PageCountArray { get => _pageCountArray; set => _pageCountArray = value; }

        [SerializeField] private int[] _MinReadTimesArray;
        public int[] MinReadTimesArray { get => _MinReadTimesArray; set => _MinReadTimesArray = value; }

        /// <summary>
        /// constructor
        /// </summary>
        public ProgressInShort(int pageNum)
        {
            _pageCountArray = new int[(pageNum-1) / 10 + 1]; 
            _MinReadTimesArray = new int[(pageNum-1) / 10 + 1];
        }

        public void SyncWithProgress(int pageNum, int[] progress)
        {
            _pageCountArray = new int[(pageNum-1) / 10 + 1];
            _MinReadTimesArray = new int[(pageNum-1) / 10 + 1];
            for (int i = 0; i < _pageCountArray.Length; ++i)
            {
                for (int j=0; j < 10; ++j)
                {
                    if (i * 10 + j > pageNum - 1)
                    {
                        break;
                    }
                    if (progress[i * 10 + j] > 0)
                    {
                        ++_pageCountArray[i];
                    }
                    if (j == 0)
                    {
                        _MinReadTimesArray[i] = progress[i * 10 + j];
                    }
                    else if (progress[i * 10 + j] < _MinReadTimesArray[i])
                    {
                        _MinReadTimesArray[i] = progress[i * 10 + j];
                    }
                }
            }
        }
    }

    [SerializeField] private int _id;
    public int Id { get => _id; }

    [SerializeField] private string _title;
    public string Title { get => _title; }

    [SerializeField] private int _pageNum;
    public int PageNum { get => _pageNum; }

    [SerializeField] private int _lastReadPage;
    public int LastReadPage { get => _lastReadPage; }

    [SerializeField] private int[] _progress;
    public int[] Progress { get => _progress; }

    [SerializeField] private ProgressInShort _progressShort;
    public ProgressInShort ProgressShort { get => _progressShort; }

    /// <summary>
    /// constructor
    /// </summary>
    public Book(int id, string title, int pageNum)
    {
        _id = id;
        _title = title;
        _pageNum = pageNum;
        _progress = new int[pageNum];
        _progressShort = new ProgressInShort(pageNum);
        _lastReadPage = 0;
    }

    public void SetProgress(int startPage, int endPage)
    {
        void SetProgressFull(int startPage, int endPage)
        {
            int[] progress = _progress;
            for (int i = startPage - 1; i < endPage; i++)
            {
                ++progress[i];
            }
            _progress = progress;
            Debug.Log(_title + "を" + startPage + "ページから" + endPage + "ページまで読んだ。");
            return;
        }

        // 変更のあった箇所のみ更新する
        void SetProgressInShort(int startPage, int endPage)
        {
            startPage--; // 1-based to 0-based
            endPage--;

            int[] pageCnt = _progressShort.PageCountArray;
            int[] minReadTimes = _progressShort.MinReadTimesArray;

            for (int i = startPage / 10; i < endPage / 10 + 1; ++i)
            {
                pageCnt[i] = 0;
                minReadTimes[i] = 0;
                for (int j = 0; j < 10; ++j)
                {
                    if (i * 10 + j > _pageNum - 1)
                    {
                        break;
                    }

                    if (Progress[i * 10 + j] > 0) // count studied page
                    {
                        ++pageCnt[i];
                    }

                    if (j == 0)
                    {
                        minReadTimes[i] = Progress[i * 10 + j]; // = Progress[i * 10]
                    }
                    else if (Progress[i * 10 + j] < minReadTimes[i])
                    {
                        minReadTimes[i] = Progress[i * 10 + j];
                    }
                }
            }
            _progressShort.PageCountArray = pageCnt;
            _progressShort.MinReadTimesArray = minReadTimes;
            return;
        }

        void SetLastReadPage(int endPage)
        {
            _lastReadPage = endPage;
            return;
        }

        SetProgressFull(startPage, endPage);
        SetProgressInShort(startPage, endPage);
        SetLastReadPage(endPage);
    }

    public void UpdateInfo(string title, int pageNum)
    {
        void UpdateProgress(int pageNum)
        {
            if (pageNum > _pageNum)
            {
                int[] deltaProgress = new List<int>(new int[pageNum - _pageNum]).ToArray();
                _progress = _progress.Concat(deltaProgress).ToArray();
            }
            else if (pageNum < _pageNum)
            {
                _progress = _progress.Take(pageNum).ToArray();
            }
        }

        if (title != "" && title != _title)
        {
            _title = title;
        }

        if (pageNum > 0 && pageNum != _pageNum)
        {
            _pageNum = pageNum;
            UpdateProgress(_pageNum);
            _progressShort.SyncWithProgress(_pageNum, _progress);
        }

        return;
    }

    public string ProgressString()
    {
        string progressString = "";
        for (int i=0; i < _progress.Length; ++i)
        {
            string color = ColorManager.GetColorForProgress(_progress[i]);
            progressString += $"<color=#{color}>■</color>";
        }
        return progressString;
    }

    public string ProgressInShortString()
    {
        string progressShortString = "";
        for (int i=0; i < _progressShort.PageCountArray.Length; ++i)
        {
            string color = ColorManager.GetColorCodeInShort(_progressShort.PageCountArray[i], _progressShort.MinReadTimesArray[i]);
            progressShortString += $"<color=#{color}>■</color>";
        }
        return progressShortString;
    }

    private static class ColorManager
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
        public static string GetColorForProgress(int progressNum)
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
}
