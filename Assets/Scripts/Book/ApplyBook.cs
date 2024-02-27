using System;
using System.Collections.Generic;
using UnityEngine;


public class ApplyBook : MonoBehaviour
{
    [SerializeField] private GameObject BookPrefab;
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject playerData;
    
    void Start()
    {
        SaveData saveData = playerData.GetComponent<PlayerData>().GetData();
        BookInfo[] book = saveData.book;

        for (int i = 0; i < book.Length; i++)
        {
            var bookPanel = Instantiate(BookPrefab, transform);

            bookPanel.GetComponent<BookPanel>().SetBook(book[i]);
            bookPanel.GetComponent<PopupTrigger>().SetId(book[i].id);
            bookPanel.GetComponent<PopupTrigger>().SetPopup(popup);
        }
    }

    public void Reload()
    {
        SaveData saveData = playerData.GetComponent<PlayerData>().GetData();
        BookInfo[] book = saveData.book;

        foreach (Transform n in gameObject.transform)
        {
            Destroy(n.gameObject);
        }

        for (int i = 0; i < book.Length; i++)
        {
            var bookPanel = Instantiate(BookPrefab, transform);

            bookPanel.GetComponent<BookPanel>().SetBook(book[i]);
            bookPanel.GetComponent<PopupTrigger>().SetId(book[i].id);
            bookPanel.GetComponent<PopupTrigger>().SetPopup(popup);
        }
    }
}
