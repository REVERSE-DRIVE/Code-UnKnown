using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(menuName = "SO/Stat")]
public class AgentStat : ScriptableObject
{
    public Stat damage;
    public Stat maxHealth;
    public Stat moveSpeed;
    
    protected Agent _owner;
    protected Dictionary<StatType, Stat> _statDictionary;

    public virtual void SetOwner(Agent owner)
    {
        _owner = owner;
    }

    public virtual void IncreaseStatFor(int value, float duration, Stat targetStat)
    {
        _owner.StartCoroutine(StatModifyCoroutine(value, duration, targetStat));
    }

    protected IEnumerator StatModifyCoroutine(int value, float duration, Stat targetStat)
    {
        targetStat.AddModifier(value);
        yield return new WaitForSeconds(duration);
        targetStat.RemoveModifier(value);
    }
    
    protected virtual void OnEnable()
    {
        _statDictionary = new Dictionary<StatType, Stat>();

        Type agentStatType = typeof(AgentStat); //이 클래스의 타입정보를 불러와서

        foreach(StatType typeEnum in Enum.GetValues(typeof(StatType)))
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
    
    // 겟 데미지, 회피, 크리티컬, 아머 등등 구현 예정

    public int GetDamage()
    {
        return damage.GetValue();
    }

    public bool CanEvasion()
    {// 민첩 1당 1퍼센트 증가
        return IsHitPercent(moveSpeed.GetValue() * 5);

    }

    
    // public bool IsCritical(ref int incomingDamage)
    // {
    //     if (IsHitPercent(criticalChance.GetValue()) )
    //     {
    //         incomingDamage = Mathf.FloorToInt(
    //             incomingDamage * criticalDamage.GetValue() * 0.0001f);
    //         return true;
    //     }
    //     return false;
    // }

    protected bool IsHitPercent(int statValue) => Random.Range(1, 10000) < statValue;


    public void AddModifier(StatType type, int value)
    {
        if (_statDictionary.TryGetValue(type, out Stat stat))
        {
            stat.AddModifier(value);
        }
        //_statDictionary[type].AddModifier(value);
    }

    public void RemoveModifier(StatType type, int value)
    {
        if (_statDictionary.TryGetValue(type, out Stat stat))
            stat.RemoveModifier(value);
        else 
            Debug.LogError($"{type.ToString()} : Modifier is Not Exist. (value:{value})");
    }
}