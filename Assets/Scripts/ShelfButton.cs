//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShelfButton : MonoBehaviour
{
    public void OnClickShelfButton()
    {
        SceneManager.LoadScene("BookShelfScene");
    }
}
