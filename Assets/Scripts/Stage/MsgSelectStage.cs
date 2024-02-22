using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MsgSelectStage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private GameObject depatureButton;

    void Start()
    {
        message.text = "ステージを選択してください。";
    }

    public void SuggestStage(int stagePointer)
    {
        message.text = "ステージ" + stagePointer + "に挑戦しますか？";

        depatureButton.GetComponent<Button>().onClick.RemoveAllListeners();
        depatureButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("ステージ" + stagePointer + "に挑戦します。");
        });
        depatureButton.SetActive(true);
    }
}
