using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopupTrigger : MonoBehaviour
{
    private int bookId;
    private GameObject popup;

    public void SetId(int id)
    {
        bookId = id;
    }

    public void SetPopup(GameObject popupPanel)
    {
        popup = popupPanel;

        Button button = GetComponent<Button>();
        button.onClick.AddListener ( () => 
        {
            Clicked();
        } );
    }

    private void Clicked()
    {
        popup.GetComponent<BookIdHolder>().SetId(bookId);
        popup.SetActive(true);
    }
}
