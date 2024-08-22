using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadUIManager : MonoBehaviour
{
    [SerializeField] private Image _loadingPregressBar;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private FadeInOut _fadeImage;

    private void Start()
    {
        _fadeImage.Fade(1f, 0f);
    }

    public void SetProgress(float progress)
    {
        _loadingPregressBar.fillAmount = progress;
        _loadingText.text = $"Loading... {Mathf.RoundToInt(progress * 100)}%";
    }
    
}