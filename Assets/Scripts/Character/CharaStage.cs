using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class CharaStage : MonoBehaviour
{
    private int charaId;
    //private Image charaImage;
    private Image charaImage;

    async void Start()
    {
        charaImage = this.GetComponent<Image>();

        charaId = PlayerPrefs.GetInt("charaId", 1);
        charaImage.sprite = await Addressables.LoadAssetAsync<Sprite>("StandingChara" + charaId + ".png").Task;
    }

    async public void SwitchCharacter(int id)
    {
        if (id == charaId)
        {
            return;
        }
        charaId = id;
        charaImage.sprite = await Addressables.LoadAssetAsync<Sprite>("StandingChara" + charaId + ".png").Task;
        PlayerPrefs.SetInt("charaId", charaId);
    }
}
