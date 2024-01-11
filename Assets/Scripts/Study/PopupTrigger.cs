using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class PopupTrigger : MonoBehaviour
{
    GameObject canvas;
    GameObject Popup;
    public int bookId;

    void Start()
    {
        canvas = transform.parent.parent.parent.parent.gameObject;
        Popup = canvas.transform.Find("ProgressPopup").gameObject;

        Button button = GetComponent<Button>();
        button.onClick.AddListener ( () => 
        {
            Clicked();
        } );
    }

    void Clicked()
    {
        Popup.GetComponent<ProgressPopup>().bookId = bookId;
        Popup.SetActive(true);
    }
}
