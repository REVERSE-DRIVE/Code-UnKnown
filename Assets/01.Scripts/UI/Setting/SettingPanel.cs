using SaveSystem;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPanel : UIPanel
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    private AudioSetting _currentSetting;
    private readonly string fileName = "AudioSetting";
    private readonly string bgmMixerName = "Volume_BGM";
    private readonly string sfxMixerName = "Volume_SFX";

    protected override void Awake()
    {
        base.Awake();
        LoadSoundSetting();
        
        _bgmSlider.onValueChanged.AddListener(HandleBGMValueChanged);
        _sfxSlider.onValueChanged.AddListener(HandleSFXValueChanged);
        
        
    }

    private void HandleBGMValueChanged(float value)
    {
        _currentSetting.bgmVolume = (int)value;
        _audioMixer.SetFloat(bgmMixerName, value);
    }

    private void HandleSFXValueChanged(float value)
    {
        _currentSetting.bgmVolume = (int)value;
        _audioMixer.SetFloat(sfxMixerName, value);
    }

    private void LoadSoundSetting()
    {
        // 오디오 세팅 데이터를 불러온다
        _currentSetting = SaveManager.Instance.Load<AudioSetting>(fileName);
        if (_currentSetting == null)
        {
            _currentSetting = new AudioSetting();
        }
    }
    

    public void SaveSoundSetting()
    {
        SaveManager.Instance.Save(_currentSetting, fileName);
    }
}
