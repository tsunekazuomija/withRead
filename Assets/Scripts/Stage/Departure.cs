using UnityEngine;
using UnityEngine.SceneManagement;

public class Departure : MonoBehaviour
{
    [SerializeField] private StageCursor stageCursor;
    [SerializeField] private ClearFlag clearFlag;

    public async void Depart()
    {
        int stage = stageCursor.StagePointer;

        var battleSystem = await SceneLoader.Load<BattleSystem>("Battle");
        battleSystem.GetStageNum(stage);
        if (clearFlag.IsCleared)
        {
            battleSystem.LoadParty(true, clearFlag.RemainingMemberList);
        }
        else
        {
            battleSystem.LoadParty(false, null);
        }
    }

    public async void Resume(int stageNum, int enemyHP)
    {
        var battleSystem = await SceneLoader.Load<BattleSystem>("Battle");
        battleSystem.GetPreviousBattleData(stageNum, enemyHP);
        if (clearFlag.IsCleared)
        {
            battleSystem.LoadParty(true, clearFlag.RemainingMemberList);
            Debug.Log("バトルの再開時にクリアフラッグがtrueになっています。");
        }
        else
        {
            battleSystem.LoadParty(false, null);
        }
    }
}