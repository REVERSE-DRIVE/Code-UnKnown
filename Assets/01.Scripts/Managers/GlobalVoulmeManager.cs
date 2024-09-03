using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVoulmeManager : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private Vignette _vignette;
    [SerializeField] private ChromaticAberration _chromaticAberration;
    private ComputerManager _computerManager;

    private void Awake()
    {
        _computerManager = ComputerManager.Instance;
        AddVolumeSettings();
        _computerManager.OnInfectionLevelChangedEvent += HandleInfectionLevelChanged;
    }

    private void AddVolumeSettings()
    {
        _volume.profile.TryGet(out _vignette);
        _volume.profile.TryGet(out _chromaticAberration);
    }

    private void HandleInfectionLevelChanged(int arg1, int arg2)
    {
        float value = arg2 / 200f;
        _vignette.intensity.value = value;
        _chromaticAberration.intensity.value = value;
    }
}