using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private Party party;

    private List<int> partyIndexList;

    private void Start()
    {
        partyIndexList = new List<int>(party.PartyMemberIndex);
        if (partyIndexList.Count == 0)
        {
            Debug.LogError("PartyManager: partyIndexList is empty.");
        }
    }

    public int GetFirstMember()
    {
        return partyIndexList[0];
    }

    public bool IsAnihilated()
    {
        return partyIndexList.Count == 0;
    }

    public void WithdrawMember()
    {
        partyIndexList.RemoveAt(0);
        return;
    }

    public void WaitInLine()
    {
        int firstMember = partyIndexList[0];
        partyIndexList.RemoveAt(0);
        partyIndexList.Add(firstMember);
        return;
    }
}
