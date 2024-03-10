using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPoint : MonoBehaviour
{
    [SerializeField] private Slider hpBar;

    public void SetHP(int level)
    {
        int hp = Calc.GetHP(level);
        hpBar.maxValue = hp;
        hpBar.value = hp;
        Debug.Log("HP: " + hp);
    }

    public void TakeDamage(int damage)
    {
        if (hpBar.value - damage < 0)
        {
            hpBar.value = 0;
        }
        else
        {
            hpBar.value -= damage;
        }
        Debug.Log("HP: " + hpBar.value);
        return;
    }

    public bool IsDead()
    {
        return hpBar.value == 0;
    }

    private static class Calc
    {
        public static int GetHP(int level)
        {
            return level * 100;
        }
    }
}
