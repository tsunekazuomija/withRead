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
    private GameObject stage;

    private int charaId;

    public async void SetCharacter(Character character)
    {
        charaId = character.id;
        charaImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Thumbnail" + charaId + ".png").Task;
        charaLevelText.text = character.level.ToString();

        Button button = GetComponent<Button>();
        button.onClick.AddListener ( () => 
        {
            Clicked();
        } );
    }

    public void SetStage(GameObject stagePanel)
    {
        stage = stagePanel;
    }

    private void Clicked()
    {
        stage.GetComponent<CharaStage>().SwitchCharacter(charaId);
    }
}
