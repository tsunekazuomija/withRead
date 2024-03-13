using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PreviousBattle : MonoBehaviour
{
    [SerializeField] bool isBattleFinished;
    public bool IsBattleFinished { get => isBattleFinished; }

    [SerializeField] bool forInit;

    [HideInInspector] [SerializeField] int stageNum;
    public int StageNum { get => stageNum; }
    [HideInInspector] [SerializeField] int enemyHP;
    public int EnemyHP { get => enemyHP; }

    void Awake()
    {
        if (forInit) return;
        Load();
    }

    public void Init()
    {
        BattleFinished();
        Save();
    }

    private void Save()
    {
        string filePath = Application.persistentDataPath + "/UserData/PreviousBattle.json";
        string jsonString = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonString);
    }

    private void Load()
    {
        string filePath = Application.persistentDataPath + "/UserData/PreviousBattle.json";
        string jsonString = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    public void BattleFinished()
    {
        isBattleFinished = true;
        stageNum = 0;
        enemyHP = 0;

        Save();
    }

    public void BattleStopped(int stage, int currentEnemyHP)
    {
        isBattleFinished = false;
        stageNum = stage;
        enemyHP = currentEnemyHP;

        Save();
    }
}
