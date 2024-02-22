using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageCursor : MonoBehaviour
{
    private int stagePointer = 0;
    [SerializeField] private MsgSelectStage msgSelectStage;

    public void SetStagePointer(int stagePointer)
    {
        this.stagePointer = stagePointer;
        msgSelectStage.SuggestStage(stagePointer);
    }

}
