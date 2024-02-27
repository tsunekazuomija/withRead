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

        Reflesh(true);
    }


    public void MemberClicked(int index)
    {
        for (int i = 0; i < partyMemberIndex.Count; i++)
        {
            if (partyMemberIndex[i] == index)
            {
                partyMemberIndex.RemoveAt(i);
                Reflesh();
                return;
            }
        }
    }

    /// <summary>
    /// called when CharacterPannel(a thmbnail of a character) is clicked.
    /// </summary>
    /// <param name="charaId"></param>
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


    private void Reflesh(bool initial=false) // todo: 冗長なのでリファクタリング
    {
        Reflect();
        if (initial)
        {
            for (int i=0; i < Party.Params.MaxMember; i++)
            {
                members[i].MemberSelect = this;
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
        else
        {
            for (int i = 0; i < Party.Params.MaxMember; i++)
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

    private void Reflect()
    {
        party.PartyMemberIndex = partyMemberIndex.ToArray();
    }
}
