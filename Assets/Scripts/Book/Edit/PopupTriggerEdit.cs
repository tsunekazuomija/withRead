using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopupTriggerEdit : MonoBehaviour
{
    private int _bookId;
    private ApplyBookEdit _applyBook;

    public void Initialize(int bookId, ApplyBookEdit ab)
    {
        GetId(bookId);
        GetObserver(ab);
    }

    private void GetId(int id)
    {
        _bookId = id;
    }

    private void GetObserver(ApplyBookEdit ab)
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
