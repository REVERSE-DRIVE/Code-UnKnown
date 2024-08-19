using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTear : MonoBehaviour
{
    Rigidbody2D rigid;
    Tilemap tilemap;
    
    Vector2 direction;
    float power;
    float fadeOutDuration = 1f;
    
    bool activeTimer = false;
    bool ready = false;

    public void RegisterThrowTile(Vector2 dir, float _power) {
        power = _power;
        direction = dir;
        
        // 랜덤 조정
        fadeOutDuration += Random.Range(-0.5f, 0.5f);
        
        ready = true;
    }

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        tilemap = GetComponentInChildren<Tilemap>();
    }

    private void Update() {
        if (activeTimer) {
            if (fadeOutDuration <= 0) {
                if (tilemap)
                    DOTween.To(() => tilemap.color, color => tilemap.color = color, new Color(1,1,1, 0), 1f).Play().OnComplete(() => Destroy(gameObject));
                else
                    Destroy(gameObject, 3f); // 페이드가 안되면 좀만 더 기다리자
                
                activeTimer = false;
            }
            fadeOutDuration -= Time.deltaTime;
        }

        if (!ready) return;
        ready = false;
        activeTimer = true;
        
        rigid.AddForce(direction * power, ForceMode2D.Impulse);
        rigid.AddTorque((100 + Random.Range(-10, 10)) * Random.Range(0, 2) == 1 ? -1 : 1, ForceMode2D.Impulse);
    }
}
