using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Character
{
    [SerializeField] private int _id;
    public int Id { get => _id; } 

    [SerializeField] private string _name;
    public string Name { get => _name; }

    [SerializeField] private int _exp;
    public int Exp { get => _exp; set => _exp = value; }

    [SerializeField] private int _level;
    public int Level { get => _level; set => _level = value; }

    [SerializeField] private bool _isUnlocked;
    public bool IsUnlocked { get => _isUnlocked; set => _isUnlocked = value; }

    [SerializeField] private int _mp; // magic point 魔力
    public int MP { get => _mp; set => _mp = value; }


    public Character(int id, string name, int exp, int level, bool isUnlocked)
    {
        _id = id;
        _name = name;
        _exp = exp;
        _level = level;
        _isUnlocked = isUnlocked;
        _mp = 0;
    }

    public int MaxMP()
    {
        return Calc.MaxMP(_level);
    }

    public int CapacityMP()
    {
        return Calc.MaxMP(_level) - _mp;
    }

    public string[] GainMagicPoint(int mp)
    {
        var messages = new List<string>();
        int mpTmp = _mp + mp;
        if (mpTmp > Calc.MaxMP(_level))
        {
            Debug.Log("Invalid input: exceeded max mp");
            return messages.ToArray();
        }

        _mp = mpTmp;
        messages.Add($"{_name} は {mp} の魔力を獲得した.\n");

        if (mpTmp == Calc.MaxMP(_level))
        {
            messages.Add($"{_name} の魔力が満タンになった.\n");
        }
        return messages.ToArray();
    }


    /// <summary>
    /// 経験値を加算する。同時にレベルアップの処理も行う。
    /// </summary>
    /// <returns>
    /// string[] 獲得した経験値、レベルアップのメッセージを返す。
    /// </returns>
    public string[] GainExp(int exp)
    {
        var messages = new List<string>();
        _exp += exp;
        messages.Add($"{_name} は {exp} の経験値を獲得した.\n");
        if (WillLevelUp(exp))
        {
            _level = Calc.CalculateLevel(_exp);
            messages.Add($"{_name} は レベル{_level} になった.\n");
        }
        return messages.ToArray();
    }

    /// <summary>
    /// 経験値を加算したときにレベルアップするかどうかを判定する。
    /// </summary>
    /// <returns>
    /// レベルアップする場合は true, しない場合は false
    /// </returns>
    private bool WillLevelUp(int exp)
    {
        int presentLevel = _level;
        int expTmp = _exp + exp;
        int nextLevel = Calc.CalculateLevel(expTmp);
        if (nextLevel < Params.MaxLevel)
        {
            return presentLevel < nextLevel;
        }
        return false;
    }

    /// <summary>
    /// 計算式をまとめたクラス。このクラスのメソッドの計算式を変えることでゲームバランスを調整できる。
    /// </summary>
    private static class Calc
    {
        public static int CalculateLevel(int exp)
        {
            return exp / 200 + 1;
        }

        public static int MaxMP(int level)
        {
            return level * 100;
        }
    }

    /// <summary>
    /// パラメータをまとめたクラス。このクラスのフィールドを変えることでゲームバランスを調整できる。
    /// </summary>
    private static class Params
    {
        public static int MaxLevel = 20;
    }
}
