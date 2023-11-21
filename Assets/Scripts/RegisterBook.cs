using UnityEngine;
using TMPro;

public class RegisterBook : MonoBehaviour
{
    TMP_InputField _inputFieldTitle;
    TMP_InputField _inputFieldNumChapter;

    public void OnclickRegisterBook()
    {
        GameObject parent = transform.parent.gameObject;
        _inputFieldTitle = parent.transform.Find("InputField-Title").GetComponent<TMP_InputField>();
        _inputFieldNumChapter = parent.transform.Find("InputField-NumChap").GetComponent<TMP_InputField>();

        string title = _inputFieldTitle.text;
        int num_chapter = int.Parse(_inputFieldNumChapter.text);
        
    }

    
}
