using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using System.IO;
using TMPro;

public class ApplyChara : MonoBehaviour
{
    [SerializeField] private GameObject charaPrefab;
    [SerializeField] private GameObject stage;

    private Character[] characters;

    void Start()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(filePath);

        SaveData saveData = JsonUtility.FromJson<SaveData>(inputString);
        characters = saveData.characters;

        foreach (var character in characters)
        {
            GameObject chara = Instantiate(charaPrefab, transform);
            chara.GetComponent<CharacterPanel>().SetCharacter(character);
            chara.GetComponent<CharacterPanel>().SetStage(stage);
        }
    }
}
