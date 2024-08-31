using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/RankPrefixSO")]
public class RankPrefixSO : ScriptableObject
{
    [System.Serializable]
    public struct Prefix {
        public string name;
        public Color color;
        public long minScore;
    }

    [SerializeField] Prefix[] prefixs;
    Prefix[] prefixs_cache;

    private void OnEnable() {
        if (prefixs_cache == null || prefixs_cache.Length != prefixs.Length)
            PrefixIndeing();
    }

    void PrefixIndeing() {
        prefixs_cache = prefixs.Clone() as Prefix[];

        // 정렬
        System.Array.Sort(prefixs_cache, (a, b) => {
            if (a.minScore > b.minScore) {
                return -1;
            } else if (a.minScore < b.minScore) {
                return 1;
            } else return 0;
        });

        Debug.Log(prefixs_cache.Length);
    }

    public Prefix GetPrefix(long score) {
        if (prefixs_cache == null || prefixs_cache.Length != prefixs.Length)
            PrefixIndeing();

        foreach (var item in prefixs_cache)
        {
            if (item.minScore <= score) {
                return item;
            }
        }

        return default; // 뭐 해야지
    }
}
