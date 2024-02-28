using System;
using System.Collections.Generic;
using UnityEngine;


public class ApplyBookProgress : MonoBehaviour
{

    [SerializeField] private GameObject BookPrefab;
    [SerializeField] private ProgressPopup progressPopup;
    [SerializeField] private BookShelf bookShelf;


    void Start()
    {
        SetOut();
    }

    public void Trrigered(int bookId)
    {
        Book book = bookShelf.BookDict[bookId];

        progressPopup.Pop(book);
    }

    public void Refresh()
    {
        foreach (Transform n in gameObject.transform)
        {
            Destroy(n.gameObject);
        }

        SetOut();
    }

    private void SetOut()
    {
        foreach (Book book in bookShelf.BookDict.Values)
        {
            var bookPanel = Instantiate(BookPrefab, transform);

            bookPanel.GetComponent<BookPanel>().SetBook(book);
            bookPanel.GetComponent<PopupTrigger>().Initialize(book.Id, this);
        }
    }
}
