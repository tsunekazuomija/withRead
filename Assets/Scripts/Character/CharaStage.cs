using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;


public class CharaStage : MonoBehaviour
{
    [SerializeField] private Party party;
    [SerializeField] private CharaBank charaBank;

    [SerializeField] private GameObject[] memberStation;
    [SerializeField] private GameObject memberPrefab;
    [SerializeField] private MPGage mpGage;


    async private void Start()
    {
        int[] partyIndexList = party.PartyMemberIndex;

        for (int i = 0; i < partyIndexList.Length; i++)
        {
            memberStation[i].SetActive(true);
        }

        for (int i = 0; i < partyIndexList.Length; i++)
        {
            GameObject member = Instantiate(memberPrefab, memberStation[i].transform);
            member.GetComponent<Image>().sprite = await Addressables.LoadAssetAsync<Sprite>("Standing" + partyIndexList[i] + ".png").Task;
            int index = i;
            member.GetComponent<Button>().onClick.AddListener(() => SwitchAttention(index));

            member.GetComponent<MemberUI>().GetAttention(i == 0);
        }
        mpGage.SetGage(charaBank.Characters[partyIndexList[0]]);
    }

    private void SwitchAttention(int index)
    {
        for (int i = 0; i < memberStation.Length; i++)
        {
            Debug.Log($"i: {i}, index: {index}");
            memberStation[i].GetComponentInChildren<MemberUI>().GetAttention(i == index);
        }
        mpGage.SetGage(charaBank.Characters[party.PartyMemberIndex[index]]);
    }
}
