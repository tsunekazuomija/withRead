using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BookIdHolder : MonoBehaviour
{
    private int bookId;

    public void SetId(int id)
    {
        bookId = id;
    }

    public int GetId()
    {
        return bookId;
    }
}
