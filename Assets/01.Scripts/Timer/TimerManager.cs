using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerManager : MonoSingleton<TimerManager>
{
    [SerializeField] GameObject UI_prefab;

    // 이벤트
    public event System.Action<int> OnChangedTime;
    public event System.Action OnFinish;
    
    RectTransform timerBox; // 만들어진거
    IEnumerator process;

    Sequence seq;

    public void ShowTimer(int time) {
        if (process != null) {
            throw new System.Exception("이미 타이머가 작동 중 입니다.");
        }

        OnChangedTime = null;
        OnFinish = null;

        if (timerBox == null) { // 안만들어져있음
            GameObject canvas = Instantiate(UI_prefab);
            canvas.name = "TimerCanvas";
            
            timerBox = canvas.transform.Find("Box") as RectTransform;

            if (timerBox == null) {
                throw new System.Exception("Timer Canvas Box 요소를 찾을 수 없음");
            }
        }

        Vector2 uiPos = timerBox.anchoredPosition;
        float lastX = uiPos.x;

        uiPos.x = -400;
        timerBox.anchoredPosition = uiPos;

        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Join(timerBox.DOAnchorPosX(lastX, 0.3f).SetEase(Ease.OutQuad));
        seq.OnComplete(() => seq = null);

        // 타이머 시작
        process = TimeProc(time);
        StartCoroutine(process);
    }

    IEnumerator TimeProc(int time) {
        // text 불러오기
        TextMeshProUGUI textObj = timerBox.GetComponentInChildren<TextMeshProUGUI>();


        while (time >= 0) {
            int min = time / 60;
            int sec = time % 60;
            textObj.text = $"{min:D2}:{sec:D2}";
            textObj.color = time <= 10 ? new Color(1f, 0.3f, 0.3f) : Color.white;

            OnChangedTime?.Invoke(time);
            if (time == 0) break;

            yield return new WaitForSeconds(1);
            time --;
        }

        // 끝
        OnFinish?.Invoke();

        CancelTimer();
        // process = null;
    }

    public void CancelTimer() {
        if (process == null) {
            Debug.LogError("진행중인 타이머가 없습니다.");
            return;
        }

        StopCoroutine(process);
        process = null;
        OnChangedTime = null;
        OnFinish = null;
        
        seq?.Kill();

        seq = DOTween.Sequence();
        seq.Join(timerBox.DOAnchorPosX(-400, 0.3f).SetEase(Ease.OutQuad));
        seq.OnComplete(() => {
            seq = null;
            Destroy(timerBox.parent.gameObject);
        });
    }
}
