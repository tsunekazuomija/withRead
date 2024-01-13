using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class StartButton : MonoBehaviour
{
    string filePath;

    public void Start()
    {
        filePath = Application.persistentDataPath + "/UserData/data.json";
    }

    public void OnClickStartButton()
    {
        if (File.Exists(filePath))
        {
            SceneManager.LoadScene("BookShelfScene");
        }
        else
        {
            SceneManager.LoadScene("UserSignUpScene");
        }   
    }
}
