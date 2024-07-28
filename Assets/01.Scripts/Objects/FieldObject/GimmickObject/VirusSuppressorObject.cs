using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSuppressorObject : MonoBehaviour, ILaserEvent
{
    List<LaserObject> lasers;

    public void Init(List<LaserObject> _lasers) {
        lasers = _lasers;
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
