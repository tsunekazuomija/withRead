using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DeleteUser : MonoBehaviour
{
    public void OnClickDeleteUser()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);

            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
        }
    }
}
