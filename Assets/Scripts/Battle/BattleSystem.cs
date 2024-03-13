using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, BUSY, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [SerializeField] private StageDatabase stageDatabase;
    [SerializeField] private CharaBank charaBank;
    [SerializeField] private Party party;
    [SerializeField] private PartyManager partyManager;
    [SerializeField] private PreviousBattle previousBattle;

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

    private bool resumed;
    private int remainingEnemyHP;  // use only when the battle is resumed

    /// <summary>
    /// Called by <c>Departure</c> in SelectStage scene
    /// </summary>
    public void GetStageNum(int num)
    {
        Debug.Log("Stage " + num);
        stageNum = num;
    }

    public void LoadParty(bool overWrite, List<int> partyIndexList)
    {
        if (overWrite)
        {
            partyManager.LoadFromList(partyIndexList);
        }
        else
        {
            partyManager.Load();
        }
    }

    public void GetPreviousBattleData(int stage, int enemyHP)
    {
        resumed = true;
        stageNum = stage;
        remainingEnemyHP = enemyHP;
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
        if (resumed)
        {
            enemyUnit.currentHP = remainingEnemyHP;
        }
        else
        {
            enemyUnit.currentHP = stageData.EnemyLevel * 100;
        }
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
        playerUnit.maxMP = charaBank.Characters[charaId].MaxMP();
        playerUnit.currentMP = charaBank.Characters[charaId].MP;
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

        playerHUD.SetMP(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        yield return PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        yield return battleDialogBox.TypeDialog("一般攻撃魔法を 使った！");
        yield return new WaitForSeconds(1f);
        bool isDead = false;
        for (int i = 0; i < 3; i++)
        {
            isDead = enemyUnit.TakeDamage(playerUnit.offense);
            enemyHUD.UpdateHP(enemyUnit.currentHP);
            yield return battleDialogBox.TypeDialog("こうげきが あたった！");
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                break;
            }
        }

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
        int skillOffense = (int) (3 * playerUnit.offense + 0.4f * playerUnit.currentMP * (1 + playerUnit.unitLevel * 0.2f));
        bool isDead = enemyUnit.TakeDamage(skillOffense);
        charaBank.SpendAllMagicPoint(playerUnit.charaId);
        playerUnit.currentMP = 0;

        yield return battleDialogBox.TypeDialog("特殊攻撃魔法を 使った！");
        enemyHUD.UpdateHP(enemyUnit.currentHP);
        playerHUD.UpdateMP(playerUnit.currentMP);

        yield return new WaitForSeconds(1f);
        yield return battleDialogBox.TypeDialog("こうげきが あたった！");

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
        playerHUD.SetMP(playerUnit);

        yield return battleDialogBox.TypeDialog(message);

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        yield return PlayerTurn();
    }

    IEnumerator EnemyTurn()
    {
        yield return battleDialogBox.TypeDialog($"{enemyUnit.unitName} の こうげき！");

        yield return new WaitForSeconds(1f);

        bool isDead = true;
        // 疲れて帰っていく

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.BUSY;
            StartCoroutine(WithdrawPlayer());
        }
        else // ここには来ない
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
            playerHUD.SetMP(playerUnit);
            state = BattleState.PLAYERTURN;

            yield return new WaitForSeconds(1f);

            yield return PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        async void Clear()
        {
            var clearFlag = await SceneLoader.Load<ClearFlag>("StageSelect");
            clearFlag.GetRemainingMember(partyManager.PartyIndexList);
            Debug.Log(partyManager.PartyIndexList.Count);
        }

        if (state == BattleState.WON)
        {
            previousBattle.BattleFinished();
            yield return battleDialogBox.TypeDialog($"{enemyUnit.unitName} に しょうりした！");
            yield return new WaitForSeconds(1f);
            yield return RegisterExperience(enemyUnit.unitLevel * 100);

            Clear();
        }
        else if (state == BattleState.LOST)
        {
            previousBattle.BattleStopped(stageNum, enemyUnit.currentHP);
            yield return battleDialogBox.TypeDialog("パーティ は ぜんめつした。");
            yield return new WaitForSeconds(1f);
            yield return battleDialogBox.TypeDialog("きかん します。");
            SceneManager.LoadScene("Study");
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
        StartCoroutine(PlayerSkillAttack());
        operationPanel.SetActive(false);
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

    private class DistExp
    {
        public int partyIndex;
        public int charaIndex;
        public int expCapacity;
        public int expGain;
        public int beforeLevel;
        public int afterLevel;
    }

    private IEnumerator RegisterExperience(int exp)
    {
        int remainingExp = exp;

        List<DistExp> distExpList = new();

        for (int i = 0; i < party.PartyMemberIndex.Length; i++)
        {
            distExpList.Add(new DistExp
            {
                partyIndex = i,
                charaIndex = party.PartyMemberIndex[i],
                expCapacity = charaBank.Characters[party.PartyMemberIndex[i]].CapacityExp(),
                expGain = 0,
                beforeLevel = charaBank.Characters[party.PartyMemberIndex[i]].Level,
                afterLevel = charaBank.Characters[party.PartyMemberIndex[i]].Level,
            });
        }


        // distExpListをCapacityExpが小さい順にソート
        for (int i = 0; i < distExpList.Count; i++)
        {
            int indexTmp = i;
            for (int j = i + 1; j < distExpList.Count; j++)
            {
                if (distExpList[j].expCapacity < distExpList[indexTmp].expCapacity)
                {
                    indexTmp = j;
                }
            }
            var tmp = distExpList[i];
            distExpList[i] = distExpList[indexTmp];
            distExpList[indexTmp] = tmp;
        }

        int remainingMember = distExpList.Count;
        for (int i=0; i<distExpList.Count; i++)
        {
            if (distExpList[i].expCapacity == 0)
            {
                // 経験値が最大まで溜まっている
                remainingMember--;
                continue;
            }

            if (remainingExp / remainingMember > distExpList[i].expCapacity)
            {
                distExpList[i].expGain = distExpList[i].expCapacity;
                remainingExp -= distExpList[i].expCapacity;
                remainingMember--;
            }
            else
            {
                distExpList[i].expGain = remainingExp / remainingMember;
                remainingExp -= distExpList[i].expGain;
                remainingMember--;
            }
        }

        // distExpListをpartyIndexが小さい順にソート
        for (int i = 0; i < distExpList.Count; i++)
        {
            int indexTmp = i;
            for (int j = i + 1; j < distExpList.Count; j++)
            {
                if (distExpList[j].partyIndex < distExpList[indexTmp].partyIndex)
                {
                    indexTmp = j;
                }
            }
            (distExpList[indexTmp], distExpList[i]) = (distExpList[i], distExpList[indexTmp]);
        }

        Debug.Log("saving exp");
        foreach (var distExp in distExpList)
        {
            charaBank.Characters[distExp.charaIndex].GainExp(distExp.expGain);
            distExp.afterLevel = charaBank.Characters[distExp.charaIndex].Level;
            string msg = $"{charaBank.Characters[distExp.charaIndex].Name} は {distExp.expGain} の経験値を得た。";
            if (distExp.beforeLevel < distExp.afterLevel)
            {
                msg += $"\nレベルが {distExp.afterLevel} に 上がった！";
            }
            yield return battleDialogBox.TypeDialog(msg);
        }
    }
}
