using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// パーティのデータを保持するクラス。パーティに所属するキャラクターのIDのみ保持すれば十分かも。
/// であれば、今のところ、このクラスは不要かも。
/// </summary>
[Serializable]
public class Party : MonoBehaviour
{
    private class Params
    {
        public int MaxMemberNum = 4;
    }
    /// <summary>
    /// パーティに所属するキャラクター.
    /// </summary>
    [SerializeField] private Character[] members;
}
