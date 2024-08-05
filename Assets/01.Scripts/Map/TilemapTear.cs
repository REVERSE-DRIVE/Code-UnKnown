using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapTear : MonoBehaviour
{
    Rigidbody2D rigid;
    
    Vector2 direction;
    float power;
    
    bool ready = false;

    public void RegisterThrowTile(Vector2 dir, float _power) {
        power = _power;
        direction = dir;
        ready = true;
    }

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!ready) return;
        ready = false;
        
        rigid.AddForce(direction * power, ForceMode2D.Impulse);
        rigid.AddTorque((100 + Random.Range(-10, 10)) * Random.Range(0, 2) == 1 ? -1 : 1, ForceMode2D.Impulse);
    }
}
