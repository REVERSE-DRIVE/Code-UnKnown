using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private FadeInOut _fadeInOut;
    [SerializeField] private LogoEffect _logoEffect;
    
    private void Start()
    {
        StartCoroutine(IntroCoroutine());
    }

    private IEnumerator IntroCoroutine()
    {
        _fadeInOut.Fade(0.5f, 0f);
        yield return new WaitForSeconds(3f);
        _fadeInOut.Fade(0.5f, 1f);
        yield return new WaitForSeconds(3f);
        _warningText.enabled = false;
        _fadeInOut.Fade(0.5f, 0f);
        _logoEffect.LogoEffectStart();
    }
}
