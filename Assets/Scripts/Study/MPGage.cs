using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MPGage : MonoBehaviour
{
    [SerializeField] private Slider MPGageSlider;
    [SerializeField] private TextMeshProUGUI MPText;

    public void SetGage(Character character)
    {
        MPGageSlider.maxValue = character.MaxMP();
        MPGageSlider.value = character.MP;
        MPText.text = $"MP: {character.MP} / {character.MaxMP()}";
    }
}
