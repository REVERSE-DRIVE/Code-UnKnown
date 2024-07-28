using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeEffectManager : MonoSingleton<VolumeEffectManager>
{
    [SerializeField] private Volume _globalVolume;
    private ColorAdjustments colorAd;

    private void Awake()
    {
        _globalVolume.profile.TryGet(out colorAd);

    }

    public void SetGrayScale(float intensity, float duration, float moveDuration)
    {
        StartCoroutine(GrayScaleCoroutine(intensity, duration, moveDuration));
    }

    private IEnumerator GrayScaleCoroutine(float intensity, float duration, float moveDuration)
    {

        float currentTime = 0;
        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            colorAd.saturation.value = Mathf.Lerp(0, intensity, currentTime / moveDuration);
            yield return null;
        }
        colorAd.saturation.value = intensity;
        yield return new WaitForSeconds(duration);
        currentTime = 0;
        while (currentTime < moveDuration)
        {
            currentTime += Time.deltaTime;
            colorAd.saturation.value = Mathf.Lerp(intensity, 0, currentTime / moveDuration);
            yield return null;
        }
        colorAd.saturation.value = 0;
    }
}
