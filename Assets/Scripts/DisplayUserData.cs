using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayUserData : MonoBehaviour
{
    [SerializeField] private GameObject playerData;

    [SerializeField] private TextMeshProUGUI uName;
    [SerializeField] private TextMeshProUGUI uLevel;
    [SerializeField] private TextMeshProUGUI uExp;


    void Start()
    {
        User pData = playerData.GetComponent<PlayerData>().GetUser();
        uName.text = pData.name;
        uLevel.text = pData.level.ToString();
        uExp.text = pData.exp.ToString();
    }

}
