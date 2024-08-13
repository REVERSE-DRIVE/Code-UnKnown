using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirusSuppressorObject : MonoBehaviour, ILaserEvent
{
    List<LaserObject> lasers;
    int redCount;

    public event Action OnClear;
    bool isClear;

    public void Init(List<LaserObject> _lasers) {
        lasers = _lasers;
        isClear = false;

        // redCount = lasers.Count(v => v.LaserType == LaserObject.Type.Red);
        redCount = 0;
        foreach (var item in lasers)
        {
            if (item.LaserType == LaserObject.Type.Red) {
                redCount ++;
                item.OnRemove += OnLaserDestory;
            }
        }
    }

    public void OnLaserIn(LaserObject entity)
    {
        lasers.Add(entity);
        CheckClear();
    }

    public void OnLaserOut(LaserObject entity)
    {
        lasers.Remove(entity);
        CheckClear();
    }
    
    void OnLaserDestory() {
        redCount --;
    }

    void CheckClear() {
        if (isClear) return;

        int green = 0, red = 0;
        
        lasers.ForEach((v) => {
            if (v.LaserType == LaserObject.Type.Green)
                green ++;
            else if (v.LaserType == LaserObject.Type.Red)
                red++;
        });

        if (green > 0 || red != redCount) return;

        isClear = true;
        OnClear?.Invoke();
    }
}
