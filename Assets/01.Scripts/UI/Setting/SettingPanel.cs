using System;
using SaveSystem;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPanel : UIPanel
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private AudioSetting _currentSetting;
    private readonly string fileName = "AudioSetting";
    private readonly string bgmMixerName = "Volume_BGM";
    private readonly string sfxMixerName = "Volume_SFX";

    protected override void Awake()
    {
        base.Awake();
        
        
    }

    private void Start()
    {
        LoadSoundSetting();
        
        _bgmSlider.onValueChanged.AddListener(HandleBGMValueChanged);
        _sfxSlider.onValueChanged.AddListener(HandleSFXValueChanged);

    }

    private void HandleBGMValueChanged(float value)
    {
        _currentSetting.bgmVolume = (int)value;
        SetBGMAudio(value);
    }

    private void HandleSFXValueChanged(float value)
    {
        _currentSetting.sfxVolume = (int)value;
        SetSFXAudio(value);
    }

    private void LoadSoundSetting()
    {
        // 오디오 세팅 데이터를 불러온다
        _currentSetting = SaveManager.Instance.Load<AudioSetting>(fileName);
        // if (_currentSetting == null)
        // {
        //     _currentSetting = new AudioSetting();
        // }
        
        _bgmSlider.value = _currentSetting.bgmVolume;
        _sfxSlider.value = _currentSetting.sfxVolume;
        _audioMixer.SetFloat(bgmMixerName, _currentSetting.bgmVolume);
        SetBGMAudio(_currentSetting.bgmVolume);
        SetSFXAudio(_currentSetting.sfxVolume);
    }
    

    public void SaveSoundSetting()
    {
        SaveManager.Instance.Save(_currentSetting, fileName);
    }
    
    private void SetBGMAudio(float level)
    {
        print("BGM level을 "+level+"로 설정했습니다.");
        _audioMixer.SetFloat(bgmMixerName, level);

    }

    private void SetSFXAudio(float level)
    {
        _audioMixer.SetFloat(sfxMixerName, level);

    }
}
