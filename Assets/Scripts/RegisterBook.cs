using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

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

        string dataPath = Application.persistentDataPath + "/UserData/data.json";
        string inputString = File.ReadAllText(dataPath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(inputString);

        int[] progress = Enumerable.Repeat(0, pages).ToArray();

        PageCell progress_short = new()
        {
            page_cnt = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray(),
            min_read_times = Enumerable.Repeat(0, Mathf.CeilToInt(pages / 10f)).ToArray()
        };

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
        File.WriteAllText(Application.persistentDataPath + "/UserData/data.json", outputString);

        _inputFieldTitle.text = "";
        _inputFieldPages.text = "";

        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}
