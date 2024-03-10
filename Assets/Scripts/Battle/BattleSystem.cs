using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, BUSY, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public TextMeshProUGUI dialogText;

    public Unit playerUnit;
    public Unit enemyUnit;

    public BattleHUD enemyHUD;

    public void GetStageNum(int stageNum)
    {
        Debug.Log("Stage " + stageNum);
    }

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        
        dialogText.text = "A wild " + enemyUnit.unitName + " approaches...";

        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.UpdateHP(enemyUnit.currentHP);
        dialogText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerSkillAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.UpdateHP(enemyUnit.currentHP);
        dialogText.text = "The attack is successful!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        } 
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = true; // playerUnit.TakeDamage(enemyUnit.damage);
        // 疲れて帰っていく

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose an action:";
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
    }

    public void OnSkillAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        state = BattleState.BUSY;
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
        return;
    }
}
