using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterButton : MonoBehaviour
{
    // NameInputFieldに文字列が入っていれば、Popupを有効にする。
    // そうでなければ、WarningMessageを表示する。
    TMP_InputField _userName;
    [SerializeField] private GameObject userNameInput;
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject confirmMessage;
    [SerializeField] private GameObject warn;


    void Start()
    {
        _userName = userNameInput.GetComponent<TMP_InputField>();
    }

    public void OnClickRegisterButton()
    {
        string userName = _userName.text;
        if (userName != "")
        {
            confirmMessage.GetComponent<TextMeshProUGUI>().text = "君の名前は\n" + userName + " ？";
            popup.SetActive(true);
        }
        else
        {
            warn.SetActive(true);
        }
    }
}
