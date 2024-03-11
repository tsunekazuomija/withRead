using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement; // forDebug

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image enemyImage;
    [SerializeField] private CharaBank _charaBank;

    private int enemyId;

    async public void Appear(StageData stageData)
    {
        Debug.Log(stageData.StageNumber);
        enemyId = stageData.EnemyId;
        enemyImage.sprite = await Addressables.LoadAssetAsync<Sprite>($"Standing{enemyId}.png").Task;
        //hitPoint.SetHP(stageData.EnemyLevel);
    }

    public string Name()
    {
        // forDebug battleシーンから開いても動くように
        if (enemyId == 0)
        {
            SceneManager.LoadScene("StageSelect");
        }
        // forDebug

        return _charaBank.Characters[enemyId].Name;
    }

    public bool TakeOrdinaryAttackDamage(int attackerId)
    {
        int attackerLevel = _charaBank.Characters[attackerId].Level;
        int damage = Calc.GetOrdAttackDamage(attackerLevel);
        // hitPoint.TakeDamage(damage);
        // return hitPoint.IsDead();
        return true;
    }

    public bool TakeSkillAttackDamage(int attackerId)
    {
        int attackerMP = _charaBank.Characters[attackerId].MP;
        int damage = Calc.GetSkillAttackDamage(attackerMP);
        // hitPoint.TakeDamage(damage);
        // return hitPoint.IsDead();
        return true;
    }

    private static class Calc
    {
        public static int AttackPower(int level)
        {
            return level * 10;
        }

        public static int GetOrdAttackDamage(int level)
        {
            float _criticalHitRate = 0.1f;
            float _criticalHitMagnification = 3f;

            int attackPower = AttackPower(level);
            
            if (Random.value < _criticalHitRate)
            {
                return (int)(attackPower * _criticalHitMagnification);
            }
            else
            {
                return attackPower;
            }
        }

        public static int GetSkillAttackDamage(int consumedMP)
        {
            float _criticalHitRate = 0.1f;
            float _criticalHitMagnification = 3f;
            float _attributeMagnification = 1f;

            int normalDamage = consumedMP * (int)_attributeMagnification;

            if (Random.value < _criticalHitRate)
            {
                return (int)(normalDamage * _criticalHitMagnification);
            }
            else
            {
                return normalDamage;
            }
        }
    }
}
