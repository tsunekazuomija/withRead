using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DeleteData : MonoBehaviour
{
    [SerializeField] private SCENE startScene;

    public void OnClickDeleteUser()
    {
        RemoveDirectory();
        SceneManager.LoadScene(startScene.ToString());
    }

    private void RemoveDirectory()
    {
        string dirPath = Application.persistentDataPath + "/UserData";
        Directory.Delete(dirPath, true);
    }
}
