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

        // generate initial progress
        string[] progress = new string[Mathf.CeilToInt(pages / 50f)];
        for (int i = 0; i < progress.Length - 1; ++i)
        {
            progress[i] = GetMultiple("0", 50);
        }
        progress[progress.Length - 1] = GetMultiple("0", pages - (progress.Length - 1) * 50);

        // generate initial progress_short
        string[] progress_short = new string[Mathf.CeilToInt(pages / 100f)];
        for (int i = 0; i < progress_short.Length - 1; ++i)
        {
            progress_short[i] = GetMultiple("0", 10);
        }
        progress_short[progress_short.Length - 1] = GetMultiple("0", Mathf.CeilToInt(pages / 10f) - (progress_short.Length - 1) * 10);

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

    string GetMultiple(string str, int num)
    {
        string result = "";
        for (int i = 0; i < num; ++i)
        {
            result += str;
        }
        return result;
    }
}
