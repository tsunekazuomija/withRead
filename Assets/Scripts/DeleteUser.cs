using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DeleteUser : MonoBehaviour
{
    [SerializeField] private SCENE startScene;

    public void OnClickDeleteUser()
    {
        DeleteData();
        DeleteCharaBank();
    }

    private void DeleteData()
    {
        string filePath = Application.persistentDataPath + "/UserData/data.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);

            SceneManager.LoadScene(startScene.ToString());
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
        }
    }

    private void DeleteCharaBank()
    {
        string filePath = Application.persistentDataPath + "/UserData/CharaBank.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
        }
    }
}
