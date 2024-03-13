using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeBattle : MonoBehaviour
{
    [SerializeField] private PreviousBattle previousBattle;
    [SerializeField] private GameObject retryPopup;
    [SerializeField] private Departure departure;

    void Start()
    {
        if (!previousBattle.IsBattleFinished)
        {
            retryPopup.SetActive(true);
        }
    }

    public void OnResume()
    {
        departure.Resume(previousBattle.StageNum, previousBattle.EnemyHP);
    }

    public void OnReset()
    {
        previousBattle.BattleFinished();
        retryPopup.SetActive(false);
    }
}
