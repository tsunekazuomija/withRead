using System;
using JetBrains.Annotations;
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
