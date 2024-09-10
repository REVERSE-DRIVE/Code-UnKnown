using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankItemUI : MonoBehaviour
{
    public struct Data {
        public int rank;
        public string name;
        public Texture2D image;
        public UnityEngine.SocialPlatforms.IUserProfile profile;
        public long score;
    }

    [SerializeField] TextMeshProUGUI nameT;
    [SerializeField] RawImage image;
    [SerializeField] TextMeshProUGUI rankT;
    [SerializeField] TextMeshProUGUI expprefixT;

    [SerializeField] RankPrefixSO rankPrefix;

    public virtual void Init(Data data) {
        nameT.text = data.name;
        if (rankT)
            rankT.text = data.rank.ToString();

        RankPrefixSO.Prefix prefix = rankPrefix.GetPrefix(data.score);
        expprefixT.text = prefix.name;
        expprefixT.color = prefix.color;
        
        if (data.profile != null)
            StartCoroutine(ImageLoadWait(data.profile));
    }

    readonly uint LOAD_TIMEOUT = 30;
    IEnumerator ImageLoadWait(UnityEngine.SocialPlatforms.IUserProfile profile) {
        uint timer = 0;
        while (profile.image == null || timer <= LOAD_TIMEOUT) {
            yield return new WaitForSeconds(1f);
            timer ++;
        }
        
        if (profile.image)
            image.texture = profile.image;
    }

    public virtual void Clear() {
        image.texture = null;
        nameT.text = "";
        if (rankT)
            rankT.text = "";
        expprefixT.text = "";
    }
}
