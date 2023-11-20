using System;
using UnityEngine;

[Serializable]
public class BookData
{
    public int id;
    public string title;
    public string author;
    public int num_chapter;
    public int[] progress;
}

[Serializable]
public class BookShelf
{
    public BookData[] Book; // 名前はJsonのキーと同じである必要がある
}

public class JsonReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string inputString = Resources.Load<TextAsset>("data").ToString();
        BookShelf bookShelf = JsonUtility.FromJson<BookShelf>(inputString);
        Debug.Log(bookShelf.Book.Length);
        Debug.Log(bookShelf.Book[0].title);
        Debug.Log(bookShelf.Book[1].progress[11]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
