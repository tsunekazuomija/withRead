using UnityEngine;
using UnityEngine.SceneManagement;

public class Departure : MonoBehaviour
{
    [SerializeField] private StageCursor stageCursor;

    public async void Depart()
    {
        int stage = stageCursor.StagePointer;
        var enemy = await SceneLoader.Load<Enemy>("Battle");
        enemy.GetStage(stage);
    }
}