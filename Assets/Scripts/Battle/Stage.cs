using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Stage : MonoBehaviour
{
    [SerializeField] private StageDatabase stageDatabase;
    [SerializeField] private Enemy enemy;


    public void GetStage(int stage)
    {
        var stageData = stageDatabase.GetStageData(stage);
        enemy.Appear(stageData);
    }
}
