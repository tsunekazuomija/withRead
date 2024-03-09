using UnityEngine;
using UnityEngine.SceneManagement;

public class Departure : MonoBehaviour
{
    private void Depart(int stage)
    {

    }

    private void putStageNum(int stage)
    {
        var enemy = GameObject.Find("Enemy");
        // enemy.GetComponent<Enemy>().SetStageNum(stage);
    }
}