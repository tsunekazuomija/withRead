using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using System.IO;
using TMPro;

public class ApplyChara : MonoBehaviour
{
    [SerializeField] private GameObject charaPrefab;

    private Character[] characters;

    async void Start()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(inputString);
        characters = saveData.characters;

        foreach (var character in characters)
        {
            var chara = Instantiate(charaPrefab, transform);
            chara.transform.GetChild(0).GetComponent<Image>().sprite = await Addressables.LoadAssetAsync<Sprite>("Thumbnail" + character.id + ".png").Task;
            chara.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = character.exp.ToString();
        }
    }
}
