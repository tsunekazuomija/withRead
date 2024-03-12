using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemberUI : MonoBehaviour
{
    public void GetAttention(bool isAttentioned)
    {
        if (isAttentioned)
        {
            // 画像を拡大
            Debug.Log("拡大");
            transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
            GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            // 画像を縮小
            Debug.Log("縮小");
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
    }
}
