using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookPanel : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject progress;

    public void SetBook(Book book)
    {
        title.GetComponent<TextMeshProUGUI>().text = book.Title;
        progress.GetComponent<TextMeshProUGUI>().text = book.ProgressInShortString();
    }
}
