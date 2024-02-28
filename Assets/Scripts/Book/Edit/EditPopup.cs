using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPopup : MonoBehaviour
{
    private BookShelf _bookShelf;
    private int _bookId;

    [SerializeField] private GameObject bookTitle;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject applyButton;
    [SerializeField] private TMP_InputField newBookTitle;
    [SerializeField] private TMP_InputField newBookPages;

    [SerializeField] private GameObject bookDisplay;
    [SerializeField] private DeletePopup deletePopup;

    [SerializeField]

    private void Awake()
    {
        deleteButton.GetComponent<Button>().onClick.AddListener ( () => deletePopup.Pop(_bookShelf, _bookId, this) );
        cancelButton.GetComponent<Button>().onClick.AddListener ( () => gameObject.SetActive(false) );
        applyButton.GetComponent<Button>().onClick.AddListener ( () => EditBook() );
    }

    public void Pop(BookShelf bookShelf, int bookId)
    {
        _bookShelf = bookShelf;
        _bookId = bookId;
        bookTitle.GetComponent<TextMeshProUGUI>().text = _bookShelf.BookDict[_bookId].Title;

        newBookTitle.placeholder.GetComponent<TextMeshProUGUI>().text = _bookShelf.BookDict[_bookId].Title;
        newBookPages.placeholder.GetComponent<TextMeshProUGUI>().text = _bookShelf.BookDict[_bookId].PageNum.ToString();
        gameObject.SetActive(true);
    }

    private void EditBook()
    {
        _bookShelf.UpdateBook(_bookId, newBookTitle.text, int.Parse(newBookPages.text));

        gameObject.SetActive(false);
        bookDisplay.GetComponent<ApplyBookEdit>().Refresh(); 
    }

    private void OnDisable()
    {
        newBookTitle.text = "";
        newBookPages.text = "";
    }
}

