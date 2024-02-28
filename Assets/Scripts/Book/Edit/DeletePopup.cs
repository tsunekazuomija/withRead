using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DeletePopup : MonoBehaviour
{
    private int _bookId;
    private BookShelf _bookShelf;

    [SerializeField] private GameObject bookTitle;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject playerData;
    [SerializeField] private EditPopup _editPopup;
    [SerializeField] private GameObject bookDisplay;

    /// <summary>
    /// Initialization. Called only once.
    /// </summary>
    void Awake()
    {
        deleteButton.GetComponent<Button>().onClick.AddListener ( () => DeleteBook() );
        cancelButton.GetComponent<Button>().onClick.AddListener ( () => gameObject.SetActive(false) );
    }

    public void Pop(BookShelf bookShelf, int bookId, EditPopup editPopup)
    {
        _bookShelf = bookShelf;
        _bookId = bookId;
        _editPopup = editPopup;
        bookTitle.GetComponent<TextMeshProUGUI>().text = bookShelf.BookDict[bookId].Title;
        gameObject.SetActive(true); // awake() is called before this line
    }

    void DeleteBook()
    {
        _bookShelf.DeleteBook(_bookId);

        gameObject.SetActive(false);
        _editPopup.gameObject.SetActive(false);
        bookDisplay.GetComponent<ApplyBookEdit>().Refresh();
    }
}
