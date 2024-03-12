using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, BUSY, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [SerializeField] private StageDatabase stageDatabase;
    [SerializeField] private CharaBank charaBank;
    [SerializeField] private PartyManager partyManager;

    public GameObject unitPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    [Header("UI")]

    [SerializeField] private BattleDialogBox battleDialogBox;
    [SerializeField] private GameObject operationPanel;

    private Unit playerUnit;
    private Unit enemyUnit;

    [SerializeField]private BattleHUD enemyHUD;
    [SerializeField]private BattleHUD playerHUD;

    private int stageNum;

    /// <summary>
    /// Called by <c>Departure</c> in SelectStage scene
    /// </summary>
    public void GetStageNum(int num)
    {
        Debug.Log("Stage " + num);
        stageNum = num;
    }

    private void SetEnemyUnit(Unit enemyUnit, int stageNum)
    {
        // ToDo: a function only for development. remove this.
        if (stageNum == 0)
        {
            Debug.Log("redirect stageNum to 1.");
            stageNum = 1;
        }

        var stageData = stageDatabase.GetStageData(stageNum);
        enemyUnit.charaId = stageData.EnemyId;
        enemyUnit.GetImage(stageData.EnemyId);
        enemyUnit.unitName = charaBank.Characters[stageData.EnemyId].Name;
        enemyUnit.unitLevel = stageData.EnemyLevel;
        enemyUnit.offense = stageData.EnemyLevel * 10;
        enemyUnit.maxHP = stageData.EnemyLevel * 100;
        enemyUnit.currentHP = stageData.EnemyLevel * 100;
    }

    private void SetPlayerUnit(Unit playerUnit)
    {
        int charaId = partyManager.GetFirstMember();
        playerUnit.charaId = charaId;
        playerUnit.GetImage(charaId);
        playerUnit.unitName = charaBank.Characters[charaId].Name;
        playerUnit.unitLevel = charaBank.Characters[charaId].Level;
        playerUnit.offense = charaBank.Characters[charaId].Level * 10;
        playerUnit.maxHP = charaBank.Characters[charaId].Level * 100;
        playerUnit.currentHP = charaBank.Characters[charaId].Level * 100;
    }

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(unitPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        SetPlayerUnit(playerUnit);

        GameObject enemyGO = Instantiate(unitPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        SetEnemyUnit(enemyUnit, stageNum);
        
        yield return battleDialogBox.TypeDialog($"{enemyUnit.unitName} が あらわれた！");

        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        yield return PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.offense);

        yield return StartCoroutine(battleDialogBox.TypeDialog("一般攻撃魔法を 使った！"));
        enemyHUD.UpdateHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(battleDialogBox.TypeDialog("こうげきが あたった！"));

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            yield return EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerSkillAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.offense);

        yield return StartCoroutine(battleDialogBox.TypeDialog("特殊攻撃魔法を 使った！"));
        enemyHUD.UpdateHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(battleDialogBox.TypeDialog("こうげきが あたった！"));

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            yield return EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        } 
    }

    IEnumerator WaitInLine()
    {
        string message = $"{playerUnit.unitName} は 列の最後尾に 戻っていった。";
        partyManager.WaitInLine();
        SetPlayerUnit(playerUnit);

        yield return StartCoroutine(battleDialogBox.TypeDialog(message));

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        yield return PlayerTurn();
    }

    IEnumerator EnemyTurn()
    {
        yield return battleDialogBox.TypeDialog($"{enemyUnit.unitName} の こうげき！");

        yield return new WaitForSeconds(1f);

        bool isDead = true; // playerUnit.TakeDamage(enemyUnit.damage);
        // 疲れて帰っていく

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.BUSY;
            StartCoroutine(WithdrawPlayer());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            yield return PlayerTurn();
        }
    }

    IEnumerator WithdrawPlayer()
    {
        partyManager.WithdrawMember();
        yield return battleDialogBox.TypeDialog($"{playerUnit.unitName} は 帰っていった。");

        yield return new WaitForSeconds(1f);

        if (partyManager.IsAnihilated())
        {
            state = BattleState.LOST;
            yield return EndBattle();
        }
        else
        {
            SetPlayerUnit(playerUnit);
            state = BattleState.PLAYERTURN;

            yield return new WaitForSeconds(1f);

            yield return PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            yield return battleDialogBox.TypeDialog($"{enemyUnit.unitName} に しょうりした！");
        }
        else if (state == BattleState.LOST)
        {
            yield return battleDialogBox.TypeDialog("パーティ は ぜんめつした。");
        }
    }

    IEnumerator PlayerTurn()
    {
        yield return battleDialogBox.TypeDialog($"{playerUnit.unitName} は どうするか考えている...");
        operationPanel.SetActive(true);
    }


    // UI Input

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        state = BattleState.BUSY; // workaround for multiple button presses
        StartCoroutine(PlayerAttack());
        operationPanel.SetActive(false);
    }

    public void OnSkillAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        state = BattleState.BUSY;
        operationPanel.SetActive(false);
        StartCoroutine(PlayerSkillAttack());
    }

    public void OnCallButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        return;
    }

    public void OnLaterButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        
        state = BattleState.BUSY;
        operationPanel.SetActive(false);
        StartCoroutine(WaitInLine());
    }
}
