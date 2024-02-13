using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private SCENE mainScene;
    [SerializeField] private SCENE signUpScene;

    [SerializeField] Button startButton;
    string filePath;

    public void Start()
    {
        filePath = Application.persistentDataPath + "/UserData/data.json";
        if (File.Exists(filePath))
        {
            startButton.onClick.AddListener( () => SceneManager.LoadScene(mainScene.ToString()) );
        }
        else
        {
            startButton.onClick.AddListener( () => SceneManager.LoadScene(signUpScene.ToString()) );
        }
    }
}
