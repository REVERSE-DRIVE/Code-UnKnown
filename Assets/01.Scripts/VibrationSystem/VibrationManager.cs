using System;
using UnityEngine;
using VibrationUtility;
using VibrationUtility.Instance;

public class VibrationManager : MonoSingleton<VibrationManager>
{

    public void Vibrate()
    {
        Handheld.Vibrate();
    }
}