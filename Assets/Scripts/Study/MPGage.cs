using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MPGage : MonoBehaviour
{
    [SerializeField] private Slider MPGageSlider;
    [SerializeField] private TextMeshProUGUI MPText;

    private Character _character;

    public void SetGage(Character character)
    {
        _character = character;
        MPGageSlider.maxValue = character.MaxMP();
        MPGageSlider.value = character.MP;
        MPText.text = $"MP: {character.MP} / {character.MaxMP()}";
    }

    public void UpdateGage()
    {
        MPGageSlider.value = _character.MP;
        MPText.text = $"MP: {_character.MP} / {_character.MaxMP()}";
    }
}
