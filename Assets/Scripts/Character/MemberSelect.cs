using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MemberSelect : MonoBehaviour
{
    [SerializeField] private PartyMember[] members;

    [SerializeField] private CharaBank charaBank;
    private List<int> partyMemberIndex = new();

    void Start()
    {
        partyMemberIndex = charaBank.PartyMemberIndex.ToList();

        Reflesh();
    }


    public void CharacterClicked(int charaId)
    {
        if (!partyMemberIndex.Contains(charaId))
        {
            if (partyMemberIndex.Count < CharaBank.Params.maxPartyMember)
            {
                partyMemberIndex.Add(charaId);
                Reflesh();
                return;
            }
            else
            {
                Debug.Log("Party is full");
                return;
            }
        }
        else
        {
            Debug.Log("Already in party");
            return;
        }
    }


    private void Reflesh()
    {
        for (int i=0; i < CharaBank.Params.maxPartyMember; i++)
        {
            if (partyMemberIndex.Count > i)
            {
                members[i].SetCharacter(partyMemberIndex[i]);
            }
            else
            {
                members[i].RemoveCharacter();
            }
        }
    }
}
