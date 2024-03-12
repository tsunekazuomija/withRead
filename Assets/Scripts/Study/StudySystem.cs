using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudySystem : MonoBehaviour
{
    [SerializeField] private Party party;
    [SerializeField] private CharaBank charaBank;
    [SerializeField] private BookShelf bookShelf;

    [Header("UI")]
    [SerializeField] private DialogBox dialogBox;
    [SerializeField] private MPGage mpGage;
    [SerializeField] private ApplyBookProgress applyBookProgress;


    public void Study(int bookId, int start, int end)
    {
        StartCoroutine(StudyCoroutine(bookId, start, end));
    }

    public IEnumerator StudyCoroutine(int bookId, int start, int end)
    {
        yield return RegisterBookProgress(bookId, start, end);
        int mp = (end - start + 1) * 5;
        yield return RegisterMagicPoint(mp);
    }

    private IEnumerator RegisterBookProgress(int bookId, int start, int end)
    {
        bookShelf.RegisterProgress(bookId, start, end);
        yield return dialogBox.TypeDialog($"{bookShelf.BookDict[bookId].Title} を {end - start + 1} ページ 読んだ。");
    }


    private class DistMP
    {
        public int partyIndex;
        public int charaIndex;
        public int mpCapacity;
        public int mpGain;
        public string message;
    }

    private IEnumerator RegisterMagicPoint(int mp)
    {
        Debug.Log("called");
        int remainingMP = mp;

        List<DistMP> distMPList = new();

        for (int i = 0; i < party.PartyMemberIndex.Length; i++)
        {
            distMPList.Add(new DistMP
            {
                partyIndex = i,
                charaIndex = party.PartyMemberIndex[i],
                mpCapacity = charaBank.Characters[party.PartyMemberIndex[i]].CapacityMP(),
                mpGain = 0,
            });
        }

        // distMPListをChapacityMPが小さい順にソート
        for (int i = 0; i < distMPList.Count; i++)
        {
            int indexTmp = i;
            for (int j = i + 1; j < distMPList.Count; j++)
            {
                if (distMPList[j].mpCapacity < distMPList[indexTmp].mpCapacity)
                {
                    indexTmp = j;
                }
            }
            var tmp = distMPList[i];
            distMPList[i] = distMPList[indexTmp];
            distMPList[indexTmp] = tmp;
        }

        int remainingMember = distMPList.Count;
        for (int i=0; i<distMPList.Count; i++)
        {
            if (distMPList[i].mpCapacity == 0)
            {
                // 魔力が最大まで溜まっている
                remainingMember--;
                continue;
            }

            if (remainingMP / remainingMember > distMPList[i].mpCapacity)
            {
                distMPList[i].mpGain = distMPList[i].mpCapacity;
                remainingMP -= distMPList[i].mpCapacity;
                remainingMember--;
            }
            else
            {
                distMPList[i].mpGain = remainingMP / remainingMember;
                remainingMP -= remainingMP / remainingMember;
                remainingMember--;
            }
        }

        // distMPListをpartyIndexが小さい順にソート
        for (int i = 0; i < distMPList.Count; i++)
        {
            int indexTmp = i;
            for (int j = i + 1; j < distMPList.Count; j++)
            {
                if (distMPList[j].partyIndex < distMPList[indexTmp].partyIndex)
                {
                    indexTmp = j;
                }
            }
            var tmp = distMPList[i];
            distMPList[i] = distMPList[indexTmp];
            distMPList[indexTmp] = tmp;
        }

        Debug.Log("saving mp");
        // 魔力をセーブ
        for (int i = 0; i < distMPList.Count; i++)
        {
            charaBank.GainMagicPoint(distMPList[i].charaIndex, distMPList[i].mpGain);
            string msg = $"{charaBank.Characters[distMPList[i].charaIndex].Name} は {distMPList[i].mpGain} の魔力を得た。";
            yield return dialogBox.TypeDialog(msg);
            yield return new WaitForSeconds(1f);
        }
        mpGage.UpdateGage();
        applyBookProgress.Refresh();
    }
}
