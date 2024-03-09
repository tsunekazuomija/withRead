using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class Enemy : MonoBehaviour
{
    [SerializeField] Image enemyImage;
    [SerializeField] private HitPoint hitPoint;

    async public void Appear(StageData stageData)
    {
        Debug.Log(stageData.StageNumber);
        enemyImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Standing" + stageData.EnemyId + ".png").Task;
        hitPoint.SetHP(stageData.EnemyLevel);
    }
}
