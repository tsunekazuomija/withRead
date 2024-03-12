using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Slider Slider;
    [SerializeField] TextMeshProUGUI Text;
    
    public void SetHUD(Unit unit)
    {
        Slider.maxValue = unit.maxHP;
        Slider.value = unit.currentHP;
        Text.text = $"HP: {unit.currentHP}/{unit.maxHP}";
    }

    public void UpdateHP(int hp)
    {
        Slider.value = hp;
        Text.text = $"HP: {hp}/{Slider.maxValue}";
    }

    public void SetMP(Unit unit)
    {
        Slider.maxValue = unit.maxMP;
        Slider.value = unit.currentMP;
        Text.text = $"MP: {unit.currentMP}/{unit.maxMP}";
    }

    public void UpdateMP(int mp)
    {
        Slider.value = mp;
        Text.text = $"MP: {mp}/{Slider.maxValue}";
    }
}
