using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Unity.Mathematics;

[Serializable]
public class Stamina : MonoBehaviour
{
    [SerializeField] Slider staminaSlider;
    private readonly int maxStamina = 180;
    // private readonly int recoveryTime = 3 * 60 * 60;
    private readonly int recoveryTime = 3 * 60 * 60;
    private string saveFilePath;


    private float timeElapsed;
    private readonly float timeOut = 10f;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/UserData/Stamina.txt";

        staminaSlider.maxValue = maxStamina;
        LoadStamina();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeOut)
        {
            Debug.Log("UpdateStamina");
            timeElapsed = 0f;
            UpdateStamina();
        }
    }

    private void UpdateStamina()
    {
        var lastZeroStaminaTime = DateTime.Parse(File.ReadAllText(saveFilePath));
        var elapsedTime = (DateTime.Now - lastZeroStaminaTime).TotalSeconds;
        var currentStamina = Math.Min(maxStamina, (int)(elapsedTime / recoveryTime * maxStamina));
        Debug.Log("currentStamina: " + currentStamina);
        staminaSlider.value = currentStamina;
    }

    public void ConsumeAllStamina()
    {
        staminaSlider.value = 0;
        File.WriteAllText(saveFilePath, DateTime.Now.ToString());
    }


    private void LoadStamina()
    {
        if (!File.Exists(saveFilePath))
        {
            CreateStaminaFile();
        }

        UpdateStamina();
    }

    public void CreateStaminaFile()
    {
        // 3 hours ago
        Debug.Log("DateTime.Now" + DateTime.Now);
        Debug.Log("DateTime.Now.AddSeconds(-recoveryTime)" + DateTime.Now.AddSeconds(-recoveryTime));
        var lastZeroStaminaTime = DateTime.Now.AddSeconds(-recoveryTime);
        File.WriteAllText(saveFilePath, lastZeroStaminaTime.ToString());
    }

    public bool IsFull()
    {
        return staminaSlider.value == maxStamina;
    }

    public int MinutesTillStaminaFull()
    {
        var lastZeroStaminaTime = DateTime.Parse(File.ReadAllText(saveFilePath));
        var elapsedTime = (DateTime.Now - lastZeroStaminaTime).TotalSeconds;
        var minutesTillStaminaFull = (int)((recoveryTime - elapsedTime) / 60);
        return minutesTillStaminaFull;
    }
}
