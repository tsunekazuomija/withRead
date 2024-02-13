using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SCENE
{
    BookSetting,
    Study,
    Start,
    SignUp,
    Character,
    UserStatus,
}

public class SceneTransition : MonoBehaviour
{

    [SerializeField] private SCENE sceneName;

    public void OnClickButton()
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
