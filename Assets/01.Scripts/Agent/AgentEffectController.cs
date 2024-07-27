using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentEffectController : MonoBehaviour
{
    public List<Effect> effectList;

    private float _currentTime = 0;
    
    
    private void Update()
    {
        _currentTime += Time.deltaTime;
        
        UpdateEffects();
    }

    public void ApplyEffect()
    {
        
    }
    
    private void UpdateEffects()
    {
        for (int i = 0; i < effectList.Count; i++)
        {
            effectList[i].Update();
        }    
    }
}