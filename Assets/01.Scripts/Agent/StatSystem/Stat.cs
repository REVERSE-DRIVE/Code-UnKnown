using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{ 
    [SerializeField] private int _baseValue;
    public List<int> modifiers;
    public bool isPercent;
    
    public int GetValue()
    {
        int total = _baseValue;
        foreach (int value in modifiers)
        {
            total += value;
        }

        return total;
    }

    public void AddModifier(int value)
    {
        if(value != 0)
            modifiers.Add(value);
    }

    public void RemoveModifier(int value)
    {
        if (value != 0)
            modifiers.Remove(value);
    }

    public void SetDefaultValue(int value)
    {
        if (value != 0)
        {
            _baseValue = value;
        }
    }

    /**
     * <summary>
     * 매개변수 %에 해당하는 스테이터스 값을 반환합니다.
     * </summary>
     */
    public int GetPercent(int percent)
    {
        return (int)((float)GetValue() / 100 * percent);
    }

}