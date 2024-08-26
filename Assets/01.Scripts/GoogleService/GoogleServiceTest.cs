using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class GoogleServiceTest : MonoBehaviour
{
    [SerializeField] Button activeBtn;
    [SerializeField] Button leaderboardBtn;
    [SerializeField] Button rankScoreBtn;
    [SerializeField] Button customRankBtn;

    public string Token;
    public string Error;

    void Awake()
    {
        activeBtn.onClick.AddListener(LoginGoogleService);    
        leaderboardBtn.onClick.AddListener(OpenLeaderBoard);
        rankScoreBtn.onClick.AddListener(SetRankScore);
        customRankBtn.onClick.AddListener(GetCustomRank);
    }

    private void GetCustomRank()
    {
        print("GetCustomRank");
        PlayGamesPlatform.Instance.LoadScores("CgkInoqooYweEAIQAg", scores => {
            print($"length: {scores.Length}");
            
            foreach (var item in scores)
            {
                print($"[{item.rank}] {item.userID} {item.value}");
            }
        });
    }

    private void SetRankScore()
    {
        print("SetRankScore");
        PlayGamesPlatform.Instance.ReportScore(2, "CgkInoqooYweEAIQAg", success => {
            print($"save Score : {success}");
        });
    }

    void LoginGoogleService() {
        //Initialize PlayGamesPlatform
        print("LoginGoogleService");

        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    private void OpenLeaderBoard()
    {
        print("OpenLeaderBoard");
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkInoqooYweEAIQAg", (status) => {
            print($"ShowLeaderboardUI status: {status.ToString()}");
        });
    }

    public void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                    // This token serves as an example to be used for SignInWithGooglePlayGames
                });
            }
            else
            {
                Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");
            }
        });
    }
}