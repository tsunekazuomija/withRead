using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private Image charaImage;
    [SerializeField] private TextMeshProUGUI charaLevelText;

    public async void SetCharacter(Character character)
    {
        int charaId = character.id;
        charaImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Thumbnail" + charaId + ".png").Task;
        charaLevelText.text = character.exp.ToString();
    }
}
