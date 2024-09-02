using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoogleAccountPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameT;
    [SerializeField] Button linkBtn;

    TextMeshProUGUI linkBtnText;

    private void Awake() {
        linkBtn.onClick.AddListener(ClickLinkBtn);
        linkBtnText = linkBtn.GetComponentInChildren<TextMeshProUGUI>();
    
        GoogleLoginSystem.OnLoginProgress += OnChangedLoginStatus;
    }

    private void Start() {
        RefreshScreen();
    }

    void ClickLinkBtn() {
        bool isLogin = PlayGamesPlatform.Instance != null && PlayGamesPlatform.Instance.IsAuthenticated();

        if (isLogin)
            GoogleLoginSystem.SetService(false);
        else
            GoogleLoginSystem.RequestGoogleLogin(true, ResultGoogleLogin); // 재시도해라
    }

    void OnChangedLoginStatus(GooglePlayGames.BasicApi.SignInStatus _) {
        RefreshScreen();
    }

    void RefreshScreen() {
        bool isLogin = PlayGamesPlatform.Instance != null && PlayGamesPlatform.Instance.IsAuthenticated();

        linkBtnText.text = isLogin ? "연동해제" : "연동하기";
        
        if (isLogin) {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            nameT.text = $"{name}님 환영합니다!";
        } else {
            nameT.text = "연동되어 있지 않음";
        }
    }

    void ResultGoogleLogin(GooglePlayGames.BasicApi.SignInStatus status) {
        if (status == GooglePlayGames.BasicApi.SignInStatus.Success) { // 로그인 성공함
            GoogleLoginSystem.SetService(true);
        }
    }
}
