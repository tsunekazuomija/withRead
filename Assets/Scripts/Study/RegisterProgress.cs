using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RegisterProgress : MonoBehaviour
{
    SaveData _saveData;

    int _siblingsIndex;

    // Start is called before the first frame update
    void Start()
    {
        string inputString = Resources.Load<TextAsset>("data").ToString();
        _saveData = JsonUtility.FromJson<SaveData>(inputString);

    }

    public void OnClickRegisterProgress()
    {
        string title = transform.parent.parent.Find("TitlePlace").Find("Text").GetComponent<TMPro.TextMeshProUGUI>().text;

        for (int i = 0; i < _saveData.book.Length; i++)
        {
            if (_saveData.book[i].title == title)
            {
                _siblingsIndex = i;
                break;
            }
        }

        int _startPage, _endPage;
        _startPage = int.Parse(transform.parent.Find("FromPageInput").GetComponent<TMPro.TMP_InputField>().text);
        _endPage = int.Parse(transform.parent.Find("ToPageInput").GetComponent<TMPro.TMP_InputField>().text);
        Debug.Log(_saveData.book[_siblingsIndex].title);
        Debug.Log(_startPage);
        Debug.Log(_endPage);

    }

}
