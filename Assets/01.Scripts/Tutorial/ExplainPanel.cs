using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ExplainPanel : MonoBehaviour
{
    [SerializeField] private bool _isPlayAwake;
    [SerializeField] private float _delay = 0;
    [SerializeField] private float _activeDuration = 0.2f;
    [SerializeField] private float _displayLifeTime = 5f;
    private FeedbackPlayer _feedbackPlayer;
    private void Awake()
    {
        _feedbackPlayer = GetComponentInChildren<FeedbackPlayer>();
        if(_isPlayAwake)
            ShowDisplay();
    }

    public void ShowDisplay()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(ShowCoroutine());

    }

    private IEnumerator ShowCoroutine()
    {
        if(_delay != 0)
            yield return new WaitForSeconds(_delay);
        Show();
        _feedbackPlayer.PlayFeedback();

        yield return new WaitForSeconds(_displayLifeTime);
        Disable();
    }

    private void Show()
    {
        transform.DOScaleX(1f, _activeDuration);
        transform.DOScaleY(1f, _activeDuration);
    }
    
    private void Disable()
    {
        transform.DOScaleX(0f, _activeDuration);
        transform.DOScaleY(0f, _activeDuration);
    }
}
