using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomPercentUtil<T>
{
    [System.Serializable]
    public struct Value {
        public int percent;
        public T entity;
    }

    Value[] percents;
    int[] cumulatives;

    public RandomPercentUtil(Value[] list) {
        percents = list;
        cumulatives = new int[list.Length];

        int total = 0;
        for (int i = 0; i < percents.Length; i++)
        {
            total += percents[i].percent;
            cumulatives[i] = total;
        }
    }

    public T GetValue() {
        int rand = Random.Range(0, 100);
        
        for (int i = 0; i < percents.Length; i++)
        {
            if (rand < cumulatives[i]) {
                return percents[i].entity;
            }
        }

        return default; // 이거 나오면 말이 안됨
    }
}
