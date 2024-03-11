using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class Unit : MonoBehaviour
{
    public int charaId;
    public string unitName;
    public int unitLevel;

    public int offense;

    public int maxHP;
    public int currentHP;

    [SerializeField] private GameObject imagePanel;

    async public void GetImage(int charaId)
    {
        imagePanel.GetComponent<Image>().sprite = await Addressables.LoadAssetAsync<Sprite>($"Standing{charaId}.png").Task;
        imagePanel.SetActive(true);
    }

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
