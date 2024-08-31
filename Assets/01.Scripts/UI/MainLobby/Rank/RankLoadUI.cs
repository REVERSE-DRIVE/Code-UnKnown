using DG.Tweening;
using UnityEngine;

public class RankLoadUI : MonoBehaviour {
    CanvasGroup canvasGroup;
    
    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show() {
        canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, 0.3f);
    }

    public void Hide() {
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, 0.3f).OnComplete(() => gameObject.SetActive(true));
    }
}