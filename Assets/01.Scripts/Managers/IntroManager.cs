using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private FadeInOut _fadeInOut;
    [SerializeField] private LogoEffect _logoEffect;

    private bool _isLogoEffect;
    
    private void Start()
    {
        StartCoroutine(IntroCoroutine());
    }

    private void Update()
    {
        if (Input.anyKeyDown && _isLogoEffect)
        {
            _fadeInOut.Fade(0.5f, 1f, () =>
            {
                SceneManager.LoadScene("TitleScene");
                _logoEffect.renderer2DData.rendererFeatures[0].SetActive(false);
            });
        }
    }

    private IEnumerator IntroCoroutine()
    {
        _isLogoEffect = false;
        _fadeInOut.Fade(0.5f, 0f);
        yield return new WaitUntil(() => Input.anyKey);
        _fadeInOut.Fade(0.5f, 1f);
        yield return new WaitForSeconds(3f);
        _warningText.enabled = false;
        _fadeInOut.Fade(0.5f, 0f);
        _isLogoEffect = true;
        yield return _logoEffect.LogoEffectStart();
        SceneManager.LoadScene("TitleScene");

    }
}
