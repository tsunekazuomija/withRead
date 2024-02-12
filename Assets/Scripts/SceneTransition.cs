using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public enum SCENE
    {
        BookSettingScene,
        BookShelfScene,
        MainScene,
        UserSignUpScene,
        CharacterScene,
        UserInfo,
    }

    [SerializeField] private SCENE sceneName;

    public void OnClickButton()
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
