using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// ToDo: Partyクラスと密に結合している。より簡潔な構造にする。
public class MemberSelect : MonoBehaviour
{
    [SerializeField] private PartyMember[] members;

    [SerializeField] private CharaBank charaBank;
    [SerializeField] private Party party;

    void Start()
    {
        Display();
    }


    public void MemberClicked(int index)
    {
        party.Remove(index);
        Reflesh();
    }

    /// <summary>
    /// called when CharacterPannel(a thmbnail of a character) is clicked.
    /// </summary>
    /// <param name="charaId"></param>
    public void CharacterClicked(int charaId)
    {
        if (!party.PartyMemberIndex.Contains(charaId))
        {
            if (party.PartyMemberIndex.Length < Party.Params.MaxMember)
            {
                party.Add(charaId);
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
        for (int i = 0; i < Party.Params.MaxMember; i++)
        {
            if (party.PartyMemberIndex.Length > i)
            {
                members[i].SetCharacter(party.PartyMemberIndex[i]);
            }
            else
            {
                members[i].RemoveCharacter();
            }
        }

    }

    private void Display()
    {
        for (int i = 0; i < Party.Params.MaxMember; i++)
        {
            members[i].MemberSelect = this;  // Refreshとの差分
            if (party.PartyMemberIndex.Length > i)
            {
                members[i].SetCharacter(party.PartyMemberIndex[i]);
            }
            else
            {
                members[i].RemoveCharacter();
            }
        }
    }
}
