using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using SaveSystem;
using UnityEngine;

[System.Serializable]
public struct GoogleSaveData {
    public bool active;
}

public class GoogleLoginSystem : MonoBehaviour
{
    ////////////////////////// static
    public static event System.Action<SignInStatus> OnLoginProgress;
    
    public static void RequestGoogleLogin(bool force = false /* true로 하면 강제적으로 로그인 창을 띄워버림!!!! */, System.Action<SignInStatus> callback = null) {
        print("Google 로그인 요청...");
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(status => ResponseLogin(status, force, callback));
    }

    static void ResponseLogin(SignInStatus status, bool force, System.Action<SignInStatus> callback) {
        print($"Google Login 상태: {status} ({(force ? "다시시도": "")})");

        if (status == SignInStatus.Success || !force)
            callback?.Invoke(status);

        if (status == SignInStatus.Success) { // d로그인 성공~~~@@!!!
            OnLoginProgress?.Invoke(SignInStatus.Success);
            return;
        }

        // 로그인을 실패함
        if (!force) { // 아 재시도 안함니다~~~
            return;
        }

        // 수동으로 행
        PlayGamesPlatform.Instance.ManuallyAuthenticate(status => ResponseLogin(status, false, callback));
    }

    public static void SetService(bool active) {
        GoogleSaveData data = new() { active = active };
        SaveManager.Instance.Save(data, saveFileName);
    }

    public static bool isLogined => PlayGamesPlatform.Instance != null && PlayGamesPlatform.Instance.IsAuthenticated();
    public static readonly string saveFileName = "GoogleService";

    ////////////////////////// instance
    private void Start() {
        if (EnabledGoogle() && (PlayGamesPlatform.Instance == null || PlayGamesPlatform.Instance.IsAuthenticated() == false)) { // google login에 동의를 한 경우
            print("Auto Google Login");
            RequestGoogleLogin();
        }
    }

    bool EnabledGoogle() {
        var data = SaveManager.Instance.Load<GoogleSaveData>(saveFileName);
        return data.active;
    }
}
