using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class Agent : MonoBehaviour
{
    [SerializeField] private CharaBank _charaBank;
    [SerializeField] private Party _party;

    [SerializeField] private Image _charaImage;

    private List<int> _partyMemberIdx;

    async void Start()
    {
        _partyMemberIdx = new List<int>(_party.PartyMemberIndex);
        _charaImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Standing" + _partyMemberIdx[0] + ".png").Task;
    }
}
