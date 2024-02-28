using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using Unity.VisualScripting;

public class PartyMember : MonoBehaviour
{
    [SerializeField] private Image charaImage;
    private bool isEmpty = true;
    public bool IsEmpty
    {
        get { return isEmpty; }
    }

    private int charaId;

    [SerializeField] private MemberSelect memberSelect;
    public MemberSelect MemberSelect
    {
        set { memberSelect = value; }
    }

    async public void SetCharacter(int id)
    {
        charaId = id;
        charaImage.sprite = await Addressables.LoadAssetAsync<Sprite>("Standing" + id + ".png").Task;
        isEmpty = false;
        charaImage.enabled = true;
    }

    public void RemoveCharacter()
    {
        charaImage.enabled = false;
        isEmpty = true;
    }

    private void Start()
    {
        this.AddComponent<Button>().onClick.AddListener(
            Clicked
        );
    }

    private void Clicked()
    {
        if (!isEmpty)
        {
            Debug.Log("Clicked " + charaId);
            memberSelect.MemberClicked(charaId);
        }
    }
}
