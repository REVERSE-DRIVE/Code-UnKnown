using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShockWaveObject : MonoBehaviour
{
    [SerializeField] float duration = 1;
    [SerializeField] SpriteRenderer _renderer;

    private void Start() {
        _renderer.DOFade(0, duration);
    }
}
