using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class DeleteUser : MonoBehaviour
{
    public void OnClickDeleteUser()
    {
        string filePath = Application.dataPath + "/Resources/data.json";
        string metaPath = filePath + ".meta";

        if (File.Exists(filePath))
        {
            FileUtil.DeleteFileOrDirectory(filePath);
            FileUtil.DeleteFileOrDirectory(metaPath); // .meta ファイルも削除する

            AssetDatabase.Refresh();
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Debug.LogWarning("File not found: " + filePath);
        }
    }
}
