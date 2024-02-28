using System;
using System.Collections.Generic;
using UnityEngine;


public class ApplyBookEdit : MonoBehaviour
{

    [SerializeField] private GameObject BookPrefab;

    [SerializeField] private EditPopup editPopup;

    [SerializeField] private BookShelf bookShelf;


    void Start()
    {
        SetOut();
    }

    public void Trrigered(int bookId)
    {
        editPopup.Pop(bookShelf, bookId);
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
            bookPanel.GetComponent<PopupTriggerEdit>().Initialize(book.Id, this);
        }
    }
}
