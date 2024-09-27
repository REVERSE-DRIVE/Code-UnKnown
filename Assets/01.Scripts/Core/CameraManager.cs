using System.Collections;
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    
    [SerializeField] private float cameraDefaultZoom = 8f;

    private Transform _defaultCameraTarget;

    private bool _isNewFollowing;
    private bool _isShaking;
    
    private void Awake()
    {
        _cinemachineBasicMultiChannelPerlin =
            _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _defaultCameraTarget = _virtualCamera.Follow;
    }

    public void Follow(Transform newTarget, float duration)
    {
        if (!_isNewFollowing)
        {
            StartCoroutine(FollowCoroutine(newTarget, duration));
        }
    }

    private IEnumerator FollowCoroutine(Transform newTarget, float duration = 1)
    {
        //GameManager.Instance._UIManager.JoyStickEnable(false);
        _virtualCamera.Follow = newTarget;
        _isNewFollowing = true;
        yield return new WaitForSeconds(duration);
        _virtualCamera.Follow = _defaultCameraTarget;
        _isNewFollowing = false;
        //GameManager.Instance._UIManager.JoyStickEnable(true);
    }

    
    public void Shake(float shakePower, float duration)
    {
        StartCoroutine(ShakeCoroutine(shakePower, duration));
    }

    private IEnumerator ShakeCoroutine(float power, float duration)
    {
        if (_isShaking) yield break;
        _isShaking = true;
        SetShake(power, power / 2);
        yield return new WaitForSeconds(duration);
        ShakeOff();
        _isShaking = false;
    }
    

    public void SetShake(float Amplitude, float frequency)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Amplitude;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;

    }

    public void ShakeOff()
    {
        SetShake(0,0);
        
    }


    #region Zooom

    


    public void StageStartCameraZoomEvent()
    {
        ZoomDefault(13, 1.5f);
    }

    public void SetZoom(float lens)
    {
        _virtualCamera.m_Lens.OrthographicSize = lens;
    }
    public void ZoomDefault()
    {
        _virtualCamera.m_Lens.OrthographicSize = cameraDefaultZoom;
    }

    public void HandleZoomCombatMode()
    {
        ZoomFromDefault(7.2f, 0.2f);
    }

    public void HandleZoomNormalMode()
    {
        ZoomDefault(0.5f);
    }

    public void ZoomDefault(float duration)
    {
        StartCoroutine(ZoomCoroutine(_virtualCamera.m_Lens.OrthographicSize, cameraDefaultZoom, duration));
    }
    
    public void ZoomDefault(float before, float duration)
    {
        StartCoroutine(ZoomCoroutine(before, cameraDefaultZoom, duration));
    }

    public void Zoom(float before, float after, float duration)
    {
        StartCoroutine(ZoomCoroutine(before, after, duration));
    }

    public void ZoomFromDefault(float targetZoom ,float duration)
    {
        StartCoroutine(ZoomCoroutine(cameraDefaultZoom, targetZoom, duration));
    }

    private IEnumerator ZoomCoroutine(float before, float after, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(before, after, t);
            yield return null;
        }
        _virtualCamera.m_Lens.OrthographicSize = after;
    }
    
    #endregion

    #region Rotation

    public void SetRotationToDefault(float rotate, float duration)
    {
        StartCoroutine(SetRotationToDefaultCoroutine(rotate, duration));
    }

    private IEnumerator SetRotationToDefaultCoroutine(float rotate, float duration)
    {
        _virtualCamera.m_Lens.Dutch = rotate;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _virtualCamera.m_Lens.Dutch = Mathf.Lerp(rotate, 0, currentTime / duration);
            yield return null;
        }

        _virtualCamera.m_Lens.Dutch = 0;
    }

    #endregion
    
}