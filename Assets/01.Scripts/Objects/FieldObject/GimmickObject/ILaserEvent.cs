using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaserEvent
{
    public void OnLaserIn(LaserObject entity);
    public void OnLaserOut(LaserObject entity);
}
