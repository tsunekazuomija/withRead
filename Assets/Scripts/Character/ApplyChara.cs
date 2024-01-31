using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class ApplyChara : MonoBehaviour
{
    [SerializeField] Image Img;
    async void Start()
    {
        var sprite = await Addressables.LoadAssetAsync<Sprite>("character2.png").Task;
        Img.sprite = sprite;
    }
}
