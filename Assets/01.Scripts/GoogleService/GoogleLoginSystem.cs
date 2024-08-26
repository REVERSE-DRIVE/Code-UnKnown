using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GoogleLoginSystem : MonoBehaviour
{
    ////////////////////////// static
    public static event System.Action<SignInStatus> OnLoginProgress;
    
    static void RequestGoogleLogin(bool force = false /* true로 하면 강제적으로 로그인 창을 띄워버림!!!! */) {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ResponseLogin);
    }

    static void ResponseLogin(SignInStatus status) {

    }

    ////////////////////////// instance
    private void Start() {
        if (true) { // google login에 동의를 한 경우
            print("");
            RequestGoogleLogin();
        }
    }
}
