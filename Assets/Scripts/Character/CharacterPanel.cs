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

    [SerializeField] private MemberSelect memberSelect;

    private int charaId;

    public async void SetCharacter(Character character)
    {
        charaId = character.Id;
        charaImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Thumbnail" + charaId + ".png").Task;
        charaLevelText.text = character.Level.ToString();

        Button button = GetComponent<Button>();
        button.onClick.AddListener ( () => 
        {
            Clicked();
        } );
    }

    public void SetMemberSelect(MemberSelect memberSelect)
    {
        this.memberSelect = memberSelect;
    }

    private void Clicked()
    {
        CharacterClicked();
    }

    private void CharacterClicked()
    {
        memberSelect.CharacterClicked(charaId);
    }
}
