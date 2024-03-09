using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class StageDatabase : ScriptableObject
{
    //public Dictionary<int, StageData> stageData = new();
    [SerializeField] private List<StageData> stageDatabase = new ();

    public StageData GetStageData(int stageNumber)
    {
        foreach (var stage in stageDatabase)
        {
            if (stage.StageNumber == stageNumber)
            {
                return stage;
            }
        }
        return null;
    }
}

[Serializable]
public class StageData
{
    [SerializeField] private int stageNumber;
    public int StageNumber { get { return stageNumber; } }

    [SerializeField] private int enemyId;
    public int EnemyId { get { return enemyId; } }

    [SerializeField] private int enemyLevel;
    public int EnemyLevel { get { return enemyLevel; } }
}