using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// This class is assigned to popup window.
/// </summary>

public class RegisterBook : MonoBehaviour
{
    public TMP_InputField title;
    public TMP_InputField pages;
    public Button registerButton;
    [SerializeField] private GameObject playerData;
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject bookList;

    [SerializeField] private BookShelf _bookShelf;

    void Start()
    {
        registerButton.onClick.AddListener( () => 
        {
            Register();
        } );
    }

    void Register()
    {
        _bookShelf.AddBook(title.text, int.Parse(pages.text));
        popup.SetActive(false);
        title.text = "";
        pages.text = "";

        bookList.GetComponent<ApplyBookEdit>().Refresh();
    }
}
