using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


public class CreateNewData : MonoBehaviour
{
    private TMP_InputField _userName;
    [SerializeField] private GameObject inputUserName;
    [SerializeField] private SCENE mainScene;

    [SerializeField] private User refUser;
    [SerializeField] private BookShelf refBookShelf;
    [SerializeField] private CharaBank refCharaBank;
    [SerializeField] private Party refParty;

    void Start()
    {
        _userName = inputUserName.GetComponent<TMP_InputField>();
    }

    public void OnClickCreateNewData()
    {
        // ディレクトリがあるか確認
        if (!Directory.Exists(Application.persistentDataPath + "/UserData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/UserData");
        }

        CreateUser();
        CreateBookShelf();
        CreateCharaBank();
        CreateParty();

        SceneManager.LoadScene(mainScene.ToString());
    }

    private void CreateUser()
    {
        refUser.Init(
            _userName.text
        );
    }

    private void CreateBookShelf()
    {
        refBookShelf.Init(
            Params.initialBook
        );
    }

    private void CreateCharaBank()
    {
        refCharaBank.Init(
            Params.initialChara
        );
    }

    private void CreateParty()
    {
        refParty.Init(
            Params.initialParty
        );
    }


    /// <summary>
    /// 初期データをまとめたクラス。
    /// </summary>
    private static class Params
    {
        public static Book[] initialBook = new Book[]
        {
            new (0, "bocchi the rock!", 100),
        };

        public static Character[] initialChara = new Character[]
        {
            new (1, "ヨム", 0, 1, true),
            new (2, "ロボ", 0, 1, true),
            new (3, "ミズ", 0, 1, true),
            new (4, "ボス", 0, 1, true),
        };

        public static int[] initialParty = new int[] { 1, }; 
    }

}
