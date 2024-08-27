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

    [SerializeField] GameObject errorAlert;
    [SerializeField] Transform list;
    [SerializeField] RankItemUI itemBox;
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
        //         score = 436
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
        
        PlayGamesPlatform.Instance.LoadScores(
            RANK_ID,
            LeaderboardStart.PlayerCentered,
            50, // 최대 50개만~~
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (datas) => {
                string[] userIds = datas.Scores.Select(v => v.userID).ToArray();
                PlayGamesPlatform.Instance.LoadUsers(userIds, (IUserProfile[] profiles) => {
                    RankItemUI.Data[] data = new RankItemUI.Data[datas.Scores.Length];

                    for (int i = 0; i < datas.Scores.Length; i++)
                    {
                        data[i] = new() {
                            rank = i + 1,
                            name = profiles[i].userName,
                            image = profiles[i].image,
                            score = datas.Scores[i].value
                        };
                    }

                    CreateItems(data);
                });
            }
        );
    }

    void CreateItems(RankItemUI.Data[] data) {
        float startY = 0;
        const float spacing = 20;

        for (int i = 0; i < data.Length; i++)
        {
            var item = Instantiate(itemBox, list);    
            item.Init(data[i]);

            // 위치 조정
            RectTransform trm = item.transform as RectTransform;
            Vector2 pos = trm.anchoredPosition;
            
            pos.y = -startY;
            trm.anchoredPosition = pos;

            startY += trm.rect.height + spacing;
        }

        (list as RectTransform).sizeDelta = new(0, startY);
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
