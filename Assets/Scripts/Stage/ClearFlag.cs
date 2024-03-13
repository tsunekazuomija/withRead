using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFlag : MonoBehaviour
{
    private bool isCleared = false;
    public bool IsCleared { get { return isCleared; } }
    
    private List<int> remainingMemberList;
    public List<int> RemainingMemberList { get { return remainingMemberList; } }

    private void SetCleared()
    {
        Debug.Log("ClearFlag: SetCleared");
        isCleared = true;
    }

    public void GetRemainingMember(List<int> remainingMemberIndexList)
    {
        remainingMemberList = remainingMemberIndexList;
        SetCleared();
    }
}
