using UnityEngine;

public class ApplyChara : MonoBehaviour
{
    [SerializeField] private GameObject charaPrefab;
    [SerializeField] private GameObject stage;
    [SerializeField] private GameObject playerData;

    private Character[] characters;

    void Start()
    {
        SaveData saveData = playerData.GetComponent<PlayerData>().GetData();
        characters = saveData.characters;

        foreach (var character in characters)
        {
            GameObject chara = Instantiate(charaPrefab, transform);
            chara.GetComponent<CharacterPanel>().SetCharacter(character);
            chara.GetComponent<CharacterPanel>().SetStage(stage);
        }
    }
}
