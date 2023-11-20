using System;
using TMPro;
using UnityEngine;



public class ApplyBook : MonoBehaviour
{
    public GameObject BookPrefab;

    public JsonReader bookShelf;

    // Start is called before the first frame update
    void Start()
    {
        string inputString = Resources.Load<TextAsset>("data").ToString();
        BookShelf bookShelf = JsonUtility.FromJson<BookShelf>(inputString);
        Debug.Log(bookShelf.Book.Length);
        for (int i = 0; i < bookShelf.Book.Length; i++)
        {
            Instantiate(BookPrefab, transform);
            GameObject bookTitle = transform.GetChild(i).GetChild(0).gameObject;
            bookTitle.GetComponent<TextMeshProUGUI>().text = bookShelf.Book[i].title;
            string progress = "";
            for (int j = 0; j < bookShelf.Book[i].progress.Length; ++j)
            {
                if (bookShelf.Book[i].progress[j] == 0)
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
        int _ChildCount = transform.childCount;
    }
}
