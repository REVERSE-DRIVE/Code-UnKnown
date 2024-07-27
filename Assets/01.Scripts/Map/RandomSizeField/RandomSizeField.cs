using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomSizeField
{
    [SerializeField] int min, max;
    [SerializeField] int width, height;
    [SerializeField] bool isRandom = false;

    public Vector2Int GetValue() {
        if (isRandom)
            return new Vector2Int(Random.Range(min, max), Random.Range(min, max));
        else
            return new Vector2Int(width, height);
    }
}
