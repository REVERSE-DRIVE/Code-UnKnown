using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleRewardManager : MonoSingleton<GoogleRewardManager>
{
    readonly string AD_ID = "ca-app-pub-3940256099942544/5224354917";

    RewardedAd rewardedAd = null;
    bool process = false;
    public bool CanUseAd => rewardedAd != null;

    private void Start() {
        LoadReward(); // 미리 광고 로딩
    }

    // 광고 로딩
    void LoadReward() {
        Debug.Log($"[Google-AD] 광고 로드 시도");
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        process = true;

        AdRequest request = new AdRequest();
        RewardedAd.Load(AD_ID, request, (RewardedAd ad, LoadAdError error) => {
            process = false;

            if (error != null || ad == null) {
                Debug.LogError($"[Google-AD] 보상 광고 불러오기 실패 {error}");
                return;
            }

            RegisterEventHandlers(ad);
            rewardedAd = ad;
        });
    }

    public void Show(System.Action callback = null) {
        if (rewardedAd == null) {
            Debug.LogError("[Google-AD] 광고를 표시할 수 없음. (1)");
            return;
        }

        if (!rewardedAd.CanShowAd()) {
            Debug.LogError("[Google-AD] 광고를 표시할 수 없음. (2)");
            return;
        }

        rewardedAd.Show((Reward reward) => {
            Debug.Log($"[Google-AD] 광고 완료 {reward.Type} / {reward.Amount}");
            callback?.Invoke();
        });
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log($"[Google-AD] 광고 수익 {adValue.Value} / {adValue.CurrencyCode}");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadReward();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadReward();
        };
    }
}
