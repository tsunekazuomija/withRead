using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class RegisterButton : MonoBehaviour
{
    // NameInputFieldに文字列が入っていれば、Popupを有効にする。
    // そうでなければ、WarningMessageを表示する。
    TMP_InputField _userName;
    GameObject _parent;
    GameObject _popup;
    GameObject _warn;
    GameObject _confirmMessage;

    void Start()
    {
        _parent = transform.parent.gameObject;
        _userName = _parent.transform.Find("NameInputField").GetComponent<TMP_InputField>();
    }

    public void OnClickRegisterButton()
    {
        string userName = _userName.text;
        if (userName != "")
        {
            // setActive(true)で表示する。
            _popup = _parent.transform.Find("Popup").gameObject;
            _confirmMessage = _popup.transform.Find("PopupScreen").gameObject.transform.Find("Message").gameObject;
            _confirmMessage.GetComponent<TextMeshProUGUI>().text = "君が、あの\n" + userName + "\nなの？";
            _popup.SetActive(true);
        }
        else
        {
            _warn = _parent.transform.Find("NameInputField").transform.Find("WarningMessage").gameObject;
            _warn.SetActive(true);
        }
    }
}
