using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] private int stageNumber;
    [SerializeField] private StageCursor stageCursor;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    public void OnClickButton()
    {
        stageCursor.SetStagePointer(stageNumber);
    }
}
