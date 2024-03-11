using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopupTrigger : MonoBehaviour
{
    private int _bookId;
    private ApplyBookProgress _applyBook;

    public void Initialize(int bookId, ApplyBookProgress ab)
    {
        GetId(bookId);
        GetObserver(ab);
    }

    private void GetId(int id)
    {
        _bookId = id;
    }

    private void GetObserver(ApplyBookProgress ab)
    {
        _applyBook = ab;

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Clicked();
        });
    }

    private void Clicked()
    {
        _applyBook.Trrigered(_bookId);
    }
}
