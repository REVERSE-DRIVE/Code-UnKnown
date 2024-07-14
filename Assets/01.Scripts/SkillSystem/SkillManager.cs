using System;
using System.Collections.Generic;
using UnityEngine;


public class SkillManager : MonoSingleton<SkillManager>
{
    private Dictionary<Type, Skill> _skills;
    private List<Skill> _enableSkillList;

    private void Awake()
    {
        _skills = new Dictionary<Type, Skill>();
        _enableSkillList = new List<Skill>();
        foreach(PlayerSkill skillEnum in Enum.GetValues(typeof(PlayerSkill)))
        {
            if (skillEnum == PlayerSkill.None) continue;

            Skill skillCompo = GetComponent($"{skillEnum.ToString()}Skill") as Skill;
            Type type = skillCompo.GetType();
            _skills.Add(type, skillCompo);
        }
    }

    public void AddEnableSkill(Skill skill)
    {
        _enableSkillList.Add(skill);
    }

    private void Update()
    {
        foreach (Skill skill in _enableSkillList)
        {
            skill.UseSkill();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //GetSkill(PlayerSkill.).UseSkill();
        }
    }

    public T GetSkill<T>() where T : Skill
    {
        Type t = typeof(T);
        if (_skills.TryGetValue(t, out Skill target))
        {
            return target as T;
        }
        return null;
    }

    public Skill GetSkill(PlayerSkill skillEnum)
    {
        Type type = Type.GetType($"{skillEnum.ToString()}Skill"); //이건 리플렉션
        if (type == null) return null;

        if (_skills.TryGetValue(type, out Skill target))
        {
            return target;
        }

        return null;
    }

}