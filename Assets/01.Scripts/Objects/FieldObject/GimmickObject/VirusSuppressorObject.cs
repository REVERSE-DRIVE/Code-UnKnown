using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VirusSuppressorObject : MonoBehaviour, ILaserEvent
{
    List<LaserObject> lasers;
    int redCount;

    public void Init(List<LaserObject> _lasers) {
        lasers = _lasers;
        redCount = lasers.Count(v => v.LaserType == LaserObject.Type.Red);
    }

    public void OnLaserIn(LaserObject entity)
    {
        print("OnLaserIn");
    }

    public void OnLaserOut(LaserObject entity)
    {
        print("OnLaserOut");
    }
}
