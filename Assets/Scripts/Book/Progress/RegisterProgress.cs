using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// progresspopupにまとめてしまうこともできるが、あまりにも長くなりそうなので分離する。
/// registerボタンに割り当てる。
/// </summary>

public class RegisterProgress : MonoBehaviour
{
    public Slider slider1; // from
    public Slider slider2; // to

    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject bookDisplay;
    [SerializeField] private GameObject messageBoard;

    [SerializeField] private BookShelf bookShelf;
    [SerializeField] private CharaBank charaBank;
    [SerializeField] private Party party;

    public void OnClickRegisterProgress()
    {
        // int _startPage = (int) slider1.value;
        // int _endPage = (int) slider2.value;
        // if (_endPage < _startPage)
        // {
        //     Debug.Log("start page num is larger than end page num");
        //     return;
        // }

        // int _bookId = popup.GetComponent<BookIdHolder>().GetId();

        // // register book progress
        // bookShelf.RegisterProgress(_bookId, _startPage, _endPage);

        // // register experience point
        // int readPage = _endPage - _startPage + 1;
        // int exp = readPage * 10;

        popup.SetActive(false);
        bookDisplay.GetComponent<ApplyBookProgress>().Refresh();
    }
}

