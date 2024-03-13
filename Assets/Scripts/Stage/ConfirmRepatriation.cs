using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmRepatriation : MonoBehaviour
{
    [SerializeField] private ClearFlag clearFlag;

    [SerializeField] private GameObject repatriationPopup;

    public void OnRepatriation()
    {
        if (clearFlag.IsCleared)
        {
            repatriationPopup.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Study");
        }
    }

    public void OnConfirmRepatriation()
    {
        SceneManager.LoadScene("Study");
    }

    public void OnCancelRepatriation()
    {
        repatriationPopup.SetActive(false);
    }
}
