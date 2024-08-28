using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    readonly string RANK_ID = "CgkInoqooYweEAIQAg";
    readonly int RANK_AMOUNT = 50;
    readonly int RANK_AMOUNT_MAX = 30;

    [SerializeField] GameObject errorAlert;
    [SerializeField] Transform list;
    [SerializeField] RankItemUI itemBox;
    [SerializeField] RankTopUI topBox;
    [SerializeField] Button closeBtn;

    private void Awake() {
        closeBtn.onClick.AddListener(Close);

        // CreateItems(new RankItemUI.Data[] {
        //     new RankItemUI.Data() {
        //         name = "도미1",
        //         rank = 1,
        //         score = 222222
        //     },
        //     new RankItemUI.Data() {
        //         name = "도미222222",
        //         rank = 2,
        //         score = 4360
        //     },
        //     new RankItemUI.Data() {
        //         name = "ㅁㄴㅇㄹ",
        //         rank = 3,
        //         score = 23
        //     } 
        // });
    }

    private void OnEnable() {
        Clear();

        if (!GoogleLoginSystem.isLogined) { // 만약 google 서비스에 연결 되어있지 않은 경우
            errorAlert.SetActive(true);
            return;   
        }

        HandleResultScore(0, null);
    }

    void HandleResultScore(int count, ScorePageToken nextToken) {
        print($"HandleResultScore {count} / {nextToken}");

        int rowCount = Mathf.Clamp(RANK_AMOUNT - count, 0, RANK_AMOUNT_MAX);
        if (rowCount == 0) return;
        
        if (count == 0) { // 0부ㅜ터 시작 하면 처음 하는거임
            PlayGamesPlatform.Instance.LoadScores(
                RANK_ID,
                LeaderboardStart.TopScores,
                rowCount, // 최대 50개만~~
                LeaderboardCollection.Public,
                LeaderboardTimeSpan.AllTime,
                (data) => AddResultScoreUI(data, () => HandleResultScore(rowCount, data.NextPageToken))
            );
            return;
        }

        PlayGamesPlatform.Instance.LoadMoreScores(
            nextToken,
            rowCount,
            (data) => AddResultScoreUI(data, () => HandleResultScore(count + rowCount, data.NextPageToken))
        );
    }

    void AddResultScoreUI(LeaderboardScoreData data, System.Action cb) {
        string[] userIds = data.Scores.Select(v => v.userID).ToArray();
        PlayGamesPlatform.Instance.LoadUsers(userIds, (IUserProfile[] profiles) => {
            RankItemUI.Data[] result = new RankItemUI.Data[data.Scores.Length];
            for (int i = 0; i < data.Scores.Length; i++)
            {
                result[i] = new() {
                    rank = data.Scores[i].rank,
                    name = profiles[i].userName,
                    profile = profiles[i],
                    score = data.Scores[i].value
                };
            }

            CreateItems(result);
            cb();
        });
    }

    void CreateItems(RankItemUI.Data[] data) {
        float sizeSum = 0;
        const float spacing = 20;

        for (int i = 0; i < data.Length; i++)
        {
            int rank = data[i].rank;
            if (rank == 1) {
                topBox.Init(data[i]); // 1등은 아주 좋은 자리로~~
                continue;
            }

            var item = Instantiate(itemBox, list);    
            item.Init(data[i]);

            // 위치 조정
            RectTransform trm = item.transform as RectTransform;
            Vector2 pos = trm.anchoredPosition;

            pos.y = -(trm.rect.height * (rank - 1 - 1));

            if (pos.y != 0)
                pos.y -= spacing * (rank - 1 - 1);

            sizeSum = pos.y - trm.rect.height;

            trm.anchoredPosition = pos;
        }

        Vector2 size = (list as RectTransform).sizeDelta;
        size = new(0, size.y - sizeSum);
        (list as RectTransform).sizeDelta = size;
    }

    void Clear() {
        for (int i = 0; i < list.childCount; i++)
            Destroy(list.GetChild(0).gameObject);

        errorAlert.SetActive(false);
    }

    void Close() {
        gameObject.SetActive(false);
    }
}
