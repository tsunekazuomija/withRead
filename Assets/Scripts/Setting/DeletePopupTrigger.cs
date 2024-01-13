using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeletePopupTrigger : MonoBehaviour
{
    GameObject canvas;
    GameObject Popup;
    public int bookId;

    void Start()
    {
        canvas = transform.parent.parent.parent.parent.parent.gameObject;
        Popup = canvas.transform.Find("DeleteBookPopup").gameObject;

        Button button = GetComponent<Button>();
        button.onClick.AddListener ( () => 
        {
            Clicked();
        } );
    }

    void Clicked()
    {
        Popup.GetComponent<DeletePopup>().bookId = bookId;
        Popup.SetActive(true);
    }
}