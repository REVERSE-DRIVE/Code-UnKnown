using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private IntroManager _introManager;
    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Fade(float duration, float targetAlpha)
    {
        _image.DOFade(targetAlpha, duration);
    }
}
