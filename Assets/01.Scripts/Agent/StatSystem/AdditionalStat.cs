using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Status/AdditionalStat")]
public class AdditionalStat : ScriptableObject
{
    public Stat bonusExp;
    public Stat bonusResource;
    public Stat attackScale; // 기본이 10이다
    //public bool canRevive;
    
    protected Agent _owner;
    protected Dictionary<AddStatType, Stat> _statDictionary;

    public virtual void SetOwner(Agent owner)
    {
        _owner = owner;
    }
    
    protected virtual void OnEnable()
    {
        _statDictionary = new Dictionary<AddStatType, Stat>();

        Type agentStatType = typeof(AdditionalStat); //이 클래스의 타입정보를 불러와서

        foreach(AddStatType typeEnum in Enum.GetValues(typeof(AddStatType)))
        {
            try
            {
                string fieldName = LowerFirstChar(typeEnum.ToString());
                FieldInfo statField = agentStatType.GetField(fieldName);
                _statDictionary.Add(typeEnum, statField.GetValue(this) as Stat);
            }catch(Exception ex)
            {
                Debug.LogError($"There are no stat - {typeEnum.ToString()} {ex.Message}");
            }
        }
    }

    private string LowerFirstChar(string input) 
        => $"{char.ToLower(input[0])}{input.Substring(1)}";

    public void AddModifier(AddStatType type, int value)
    {
        if (_statDictionary.TryGetValue(type, out Stat stat))
        {
            stat.AddModifier(value);
        }
        //_statDictionary[type].AddModifier(value);
    }

    public void RemoveModifier(AddStatType type, int value)
    {
        if (_statDictionary.TryGetValue(type, out Stat stat))
            stat.RemoveModifier(value);
        else 
            Debug.LogError($"{type.ToString()} : Modifier is Not Exist. (value:{value})");
    }
    
}