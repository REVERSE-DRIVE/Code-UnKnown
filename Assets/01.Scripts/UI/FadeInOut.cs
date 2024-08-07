using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Fade(float duration, float targetAlpha, TweenCallback onComplete = null)
    {
        _image.DOFade(targetAlpha, duration).OnComplete(onComplete);
    }
}
