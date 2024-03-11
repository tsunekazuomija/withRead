using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayUserData : MonoBehaviour
{
    [SerializeField] private User user;

    [SerializeField] private TextMeshProUGUI uName;
    [SerializeField] private TextMeshProUGUI uLevel;
    [SerializeField] private TextMeshProUGUI uExp;


    void Start()
    {
        uName.text = user.Name;
        uLevel.text = user.Level.ToString();
        uExp.text = user.Exp.ToString();
    }

}
