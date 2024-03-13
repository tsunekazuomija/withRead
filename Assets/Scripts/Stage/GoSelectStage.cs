using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSelectStage : MonoBehaviour
{
    [SerializeField] private Stamina stamina;
    [SerializeField] private DialogBox dialogBox;

    public void OnClicked()
    {
        if (!stamina.IsFull())
        {
            string msg = "スタミナ が 足りません。\n";
            msg += $"あと {stamina.MinutesTillStaminaFull()} 分 で スタミナが 回復します。";
            StartCoroutine(dialogBox.TypeDialog(msg));
            return;
        }
        SceneManager.LoadScene("StageSelect");
    }
}