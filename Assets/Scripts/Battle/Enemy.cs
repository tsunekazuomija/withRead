using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetStage(int stage)
    {
        Debug.Log("Stage: " + stage);
    }
}
