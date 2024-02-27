using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class PartyMember : MonoBehaviour
{
    [SerializeField] private Image charaImage;
    private bool isEmpty = true;
    public bool IsEmpty
    {
        get { return isEmpty; }
    }

    async public void SetCharacter(int id)
    {
        charaImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Standing" + id + ".png").Task;
        isEmpty = false;
    }

    public void RemoveCharacter()
    {
        charaImage.sprite = null;
        isEmpty = true;
    }
}
