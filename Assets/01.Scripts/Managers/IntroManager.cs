using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        yield return new WaitUntil(() => Input.anyKey);
        _fadeInOut.Fade(0.5f, 1f);
        yield return new WaitForSeconds(3f);
        _warningText.enabled = false;
        _fadeInOut.Fade(0.5f, 0f);
        yield return _logoEffect.LogoEffectStart();
        SceneManager.LoadScene("TitleScene");

    }
}
