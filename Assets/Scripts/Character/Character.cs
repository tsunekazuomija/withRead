using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Character
{
    [SerializeField] private int _id;
    public int Id {
        get { return _id; }
    }

    [SerializeField] private string _name;
    public string Name
    {
        get { return _name; }
    }

    [SerializeField] private int _exp;
    public int Exp
    {
        get { return _exp; }
        set { _exp = value; }
    }

    [SerializeField] private int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    [SerializeField] private bool _isUnlocked;
    public bool IsUnlocked
    {
        get { return _isUnlocked; }
        set { _isUnlocked = value; }
    }

    public Character(int id, string name, int exp, int level, bool isUnlocked)
    {
        _id = id;
        _name = name;
        _exp = exp;
        _level = level;
        _isUnlocked = isUnlocked;
    }
}
