using UnityEngine;
using UnityEngine.SceneManagement;

public class Departure : MonoBehaviour
{
    [SerializeField] private StageCursor stageCursor;

    public async void Depart()
    {
        int stage = stageCursor.StagePointer;
        var battleSystem = await SceneLoader.Load<BattleSystem>("Battle");
        battleSystem.GetStageNum(stage);
    }

    public async void Resume(int stageNum, int enemyHP)
    {
        var battleSystem = await SceneLoader.Load<BattleSystem>("Battle");
        battleSystem.GetPreviousBattleData(stageNum, enemyHP);
    }
}