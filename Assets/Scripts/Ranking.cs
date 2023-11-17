using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    string[] rankNames = {"1st", "2nd", "3rd"};
    const int rankCnt = SaveData.rankCnt;

    Text[] rankTexts = new Text[rankCnt];
    SaveData data;
    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<DataManager>().data;

        for (int i = 0; i < rankCnt; i++)
        {
            Transform rankChilds = GameObject.Find("rankTexts").transform.GetChild(i);
            rankTexts[i] = rankChilds.GetComponent<Text>();
        }   
    }

    void FixedUpdate()
    {
        DispRank();
    }

    void DispRank()
    {
        for (int i = 0; i < rankCnt; i++)
        {
            rankTexts[i].text = rankNames[i] + " : " + data.rank[i];
        }
    }

    public void SetRank()
    {
        InputField inpFld = GameObject.Find("InputField").GetComponent<InputField>();
        int score = int.Parse(inpFld.text);
        for (int i = 0; i < rankCnt; ++i)
        {
            if (score > data.rank[i])
            {
                var rep = data.rank[i];
                data.rank[i] = score;
                score = rep;
            }
        }
    }

    public void DelRank()
    {
        for (int i = 0; i < rankCnt; ++i)
        {
            data.rank[i] = 0;
        }
    }
}
