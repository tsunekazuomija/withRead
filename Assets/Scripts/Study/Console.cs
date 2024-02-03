using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    private TextMeshProUGUI message;

    void Start()
    {
        message = GetComponent<TextMeshProUGUI>();
        message.text = "";
    }

    public void SetMessage(string m)
    {
        message.text = m;
    }
}
