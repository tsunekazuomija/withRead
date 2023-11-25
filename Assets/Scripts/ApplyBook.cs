using System;
using TMPro;
using UnityEngine;



public class ApplyBook : MonoBehaviour
{
    public GameObject BookPrefab;

    public JsonReader bookShelf;
    public JsonReader Data;


    // Start is called before the first frame update
    void Start()
    {
        string inputString = Resources.Load<TextAsset>("data").ToString();
        SaveData saveData = JsonUtility.FromJson<SaveData>(inputString);
        BookData[] book = saveData.book;
        for (int i = 0; i < book.Length; i++)
        {
            Instantiate(BookPrefab, transform);
            GameObject bookTitle = transform.GetChild(i).GetChild(0).gameObject;
            bookTitle.GetComponent<TextMeshProUGUI>().text = book[i].title;
            string progress = "";
            for (int j = 0; j < book[i].progress.Length; ++j)
            {
                if (book[i].progress[j] == 0)
                {
                    progress += "-";
                }
                else
                {
                    progress += "+";
                }
            }
            GameObject bookProgress = transform.GetChild(i).GetChild(1).gameObject;
            bookProgress.GetComponent<TextMeshProUGUI>().text = progress;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
