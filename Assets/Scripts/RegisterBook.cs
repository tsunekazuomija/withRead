using UnityEngine;
using TMPro;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;

public class RegisterBook : MonoBehaviour
{
    TMP_InputField _inputFieldTitle;
    TMP_InputField _inputFieldPages;

    public void OnclickRegisterBook()
    {
        GameObject parent = transform.parent.gameObject;
        _inputFieldTitle = parent.transform.Find("InputField-Title").GetComponent<TMP_InputField>();
        _inputFieldPages = parent.transform.Find("InputField-Pages").GetComponent<TMP_InputField>();

        string title = _inputFieldTitle.text;
        int pages = int.Parse(_inputFieldPages.text);

        string inputString = Resources.Load<TextAsset>("data").ToString();
        SaveData saveData = JsonUtility.FromJson<SaveData>(inputString);

        int[] progress = Enumerable.Repeat(0, pages).ToArray();

        PageCell progress_short = new PageCell();
        progress_short.page_cnt = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray();
        progress_short.min_read_times = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray();

        BookInfo newBook = new()
        {
            id = saveData.book.Last().id + 1,
            title = title,
            pages = pages,
            progress = progress,
            progress_short = progress_short,
        };

        saveData.book = saveData.book.Concat(new BookInfo[] { newBook }).ToArray();
        string outputString = JsonUtility.ToJson(saveData, true);
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/data.json", outputString);

        _inputFieldTitle.text = "";
        _inputFieldPages.text = "";
        AssetDatabase.Refresh();

        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}
