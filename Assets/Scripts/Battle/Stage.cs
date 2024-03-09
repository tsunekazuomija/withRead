using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private StageDatabase stageDatabase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetStage(int stage)
    {
        var stageData = stageDatabase.GetStageData(stage);
        Debug.Log(stageData.StageNumber);
        Debug.Log(stageData.EnemyId);
        Debug.Log(stageData.EnemyLevel);
    }
}
