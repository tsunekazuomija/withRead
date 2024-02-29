using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private SCENE mainScene;
    [SerializeField] private SCENE signUpScene;

    [SerializeField] Button startButton;

    public void Start()
    {
        string dirPath = Application.persistentDataPath + "/UserData";
        if (Directory.Exists(dirPath))
        {
            startButton.onClick.AddListener( () => SceneManager.LoadScene(mainScene.ToString()) );
        }
        else
        {
            startButton.onClick.AddListener( () => SceneManager.LoadScene(signUpScene.ToString()) );
        }
    }
}
