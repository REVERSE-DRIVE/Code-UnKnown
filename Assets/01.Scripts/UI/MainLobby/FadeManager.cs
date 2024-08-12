using System;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private FadeInOut _fadeInOut;
    private void Start()
    {
        _fadeInOut.Fade(1f, 0f);
    }
}