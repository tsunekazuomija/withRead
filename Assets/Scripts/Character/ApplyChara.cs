using System.Collections.Generic;
using UnityEngine;

public class ApplyChara : MonoBehaviour
{
    [SerializeField] private GameObject charaPrefab;
    [SerializeField] private GameObject stage;
    [SerializeField] private CharaBank charaBank;

    private Dictionary<int, Character> characters;

    void Start()
    {
        characters = charaBank.Characters;

        foreach (var character in characters.Values)
        {
            if (character.IsUnlocked)
            {
                GameObject chara = Instantiate(charaPrefab, transform);
                chara.GetComponent<CharacterPanel>().SetCharacter(character);
                chara.GetComponent<CharacterPanel>().SetStage(stage);
            }
        }
    }
}
