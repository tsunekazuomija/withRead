using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditBookInfo : MonoBehaviour
{
    private int bookId = -1;

    [SerializeField] BookIdHolder bookIdHolder;

    private SaveData _saveData;
    private BookInfo[] _book;

    [SerializeField] private GameObject bookTitle;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject applyButton;
    [SerializeField] private GameObject playerData;
    [SerializeField] private TMP_InputField newBookTitle;
    [SerializeField] private TMP_InputField newBookPages;

    [SerializeField] private GameObject bookDisplay;
    [SerializeField] private GameObject deletePopup;

    private void Awake()
    {
        deleteButton.GetComponent<Button>().onClick.AddListener ( () => deletePopup.SetActive(true) );
        cancelButton.GetComponent<Button>().onClick.AddListener ( () => gameObject.SetActive(false) );
        applyButton.GetComponent<Button>().onClick.AddListener ( () => EditBook() );
    }

    private void OnEnable()
    {
        _saveData = playerData.GetComponent<PlayerData>().GetData();
        _book = _saveData.book;

        bookId = bookIdHolder.GetId();
        int targetIndex = GetIndexFromId(bookId);
        bookTitle.GetComponent<TextMeshProUGUI>().text = _book[targetIndex].title;

        newBookTitle.placeholder.GetComponent<TextMeshProUGUI>().text = _book[targetIndex].title;
        newBookPages.placeholder.GetComponent<TextMeshProUGUI>().text = _book[targetIndex].pages.ToString();
    }

    private void EditBook()
    {
        int targetIndex = GetIndexFromId(bookId);

        if (newBookTitle.text != "")
        {
            _book[targetIndex].title = newBookTitle.text;
        }
        if (newBookPages.text != "")
        {
            int pagesPast = _book[targetIndex].pages;
            int pagesNew = int.Parse(newBookPages.text);

            if (pagesNew > pagesPast)
            {
                _book[targetIndex].pages = pagesNew;

                int[] deltaProgress = new List<int>(Enumerable.Repeat(0, pagesNew - pagesPast)).ToArray();
                _book[targetIndex].progress = _book[targetIndex].progress.Concat(deltaProgress).ToArray();
            }
            else if (pagesNew < pagesPast)
            {
                _book[targetIndex].pages = pagesNew;

                _book[targetIndex].progress = _book[targetIndex].progress.Take(pagesNew).ToArray();
            }

            _book[targetIndex].progress_short = GetProgressShortFromProgress(_book[targetIndex].progress, pagesNew);

        }
        _saveData.book = _book;
        playerData.GetComponent<PlayerData>().SetData(_saveData);

        gameObject.SetActive(false);
        bookDisplay.GetComponent<ApplyBook>().Reload(); 
    }

    private int GetIndexFromId(int id)
    {
        for (int i=0; i<_book.Length; ++i)
        {
            if (_book[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }

    private PageCell GetProgressShortFromProgress(int[] progress, int pagenum)
    {
        int[] page_cnt = new List<int>(Enumerable.Repeat(0, (pagenum-1) / 10 + 1)).ToArray();
        int[] min_read_times = new List<int>(Enumerable.Repeat(0, (pagenum-1) / 10 + 1)).ToArray();

        for (int idx=0; idx<page_cnt.Length; ++idx)
        {
            for (int i=0; i<10; ++i)
            {
                if (idx * 10 + i > pagenum - 1)
                {
                    break;
                }
                if (progress[idx * 10 + i] > 0)
                {
                    ++page_cnt[idx];
                }
                if (i == 0)
                {
                    min_read_times[idx] = progress[idx * 10 + i];
                }
                else
                {
                    min_read_times[idx] = Mathf.Min(min_read_times[idx], progress[idx * 10 + i]);
                }
            }
        }

        PageCell progressShort = new()
        {
            page_cnt = page_cnt,
            min_read_times = min_read_times
        };

        return progressShort;
    }

    private void OnDisable()
    {
        newBookTitle.text = "";
        newBookPages.text = "";
    }
}

