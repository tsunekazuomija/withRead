using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using System;

public class ConsumeStamina : MonoBehaviour
{
    private string saveFilePath;
    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/UserData/Stamina.txt";
    }

    public void ConsumeAllStamina()
    {
        File.WriteAllText(saveFilePath, DateTime.Now.ToString());
    }
}