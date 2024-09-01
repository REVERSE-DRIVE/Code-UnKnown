using DG.Tweening;
using UnityEngine;

public class RankLoadUI : MonoBehaviour {
    CanvasGroup canvasGroup;
    
    private void Awake() {
    }

    public void Show() {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, 0.3f);
    }

    public void Hide() {
        canvasGroup.DOKill();
        canvasGroup.DOFade(0, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
}