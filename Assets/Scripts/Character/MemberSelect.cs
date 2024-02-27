using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MemberSelect : MonoBehaviour
{
    [SerializeField] private PartyMember[] members;

    [SerializeField] private CharaBank charaBank;
    [SerializeField] private Party party;
    private List<int> partyMemberIndex = new();

    void Start()
    {
        partyMemberIndex = party.PartyMemberIndex.ToList();

        Reflesh();
    }


    public void CharacterClicked(int charaId)
    {
        if (!partyMemberIndex.Contains(charaId))
        {
            if (partyMemberIndex.Count < Party.Params.MaxMember)
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
        for (int i=0; i < Party.Params.MaxMember; i++)
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
