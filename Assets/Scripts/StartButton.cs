using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private string FilePath = "Assets/Resources/data.json";

    public void OnClickStartButton()
    {
        if (System.IO.File.Exists(FilePath))
        {
            SceneManager.LoadScene("BookShelfScene");
        }
        else
        {
            SceneManager.LoadScene("UserSignUpScene");
        }   
    }
}
