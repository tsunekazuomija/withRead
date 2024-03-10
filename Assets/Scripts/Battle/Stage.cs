using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public enum BattleState2
{
    Start,
    PlayerAction,
    EnemyAction,
    Busy,
    End,
}

public class Stage : MonoBehaviour
{
    [SerializeField] private StageDatabase stageDatabase;
    [SerializeField] private Agent agent;
    [SerializeField] private Enemy enemy;
    [SerializeField] private BattleDialogBox battleDialogBox;
    [SerializeField] private GameObject OperationPanel;

    [SerializeField] private Button ordinaryAttackButton;
    [SerializeField] private Button skillAttackButton;
    [SerializeField] private Button callButton;
    [SerializeField] private Button postponeButton;

    private BattleState2 state;

    private void Start()
    {
        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle()
    {
        state = BattleState2.Start;
        yield return StartCoroutine(battleDialogBox.TypeDialog($"{enemy.Name()} が あらわれた！\n{agent.Name()}は どうする？"));
        yield return new WaitForSeconds(1f);
        PlayerAction();
    }

    // called from stage select scene
    public void GetStage(int stage)
    {
        var stageData = stageDatabase.GetStageData(stage);
        agent.Appear();
        enemy.Appear(stageData);
    }


    private void PlayerAction()
    {
        state = BattleState2.PlayerAction;
        OperationPanel.SetActive(true);

        ordinaryAttackButton.onClick.AddListener(() => PerformPlayerAction(0));
    }

    private void PerformPlayerAction(int action)
    {
        state = BattleState2.Busy;

        switch (action)
        {
            case 0:
                StartCoroutine(OrdinaryAttack());
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }

        IEnumerator OrdinaryAttack()
        {
            StartCoroutine(battleDialogBox.TypeDialog($"{agent.Name()} は 一般攻撃魔法 を つかった！"));

            // enemyダメージ計算
            bool isDead = enemy.TakeOrdinaryAttackDamage(agent.Id());

            if (isDead)
            {
                yield return battleDialogBox.TypeDialog($"{enemy.Name()} は たおれた");
            }
            else
            {
                // enemy moveへ
            }
        }

        IEnumerator SkillAttack()
        {
            StartCoroutine(battleDialogBox.TypeDialog($"{agent.Name()} は 特殊攻撃魔法 を つかった！"));

            bool isDead = enemy.TakeSkillAttackDamage(agent.Id());

            if (isDead)
            {
                yield return battleDialogBox.TypeDialog($"{enemy.Name()} は たおれた");
            }
            else
            {
                // enemy moveへ
            }
        }
    }
}
