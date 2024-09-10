using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleObject : MonoBehaviour
{
    const string COLOR_MAT_ID = "_Color";
    const string BLINK_MAT_ID = "_UseBlink";

    [SerializeField] Transform fixedPos;
    
    [Header("visual")]
    [SerializeField, ColorUsage(true, true)] Color checkColor;
    [SerializeField] SpriteRenderer _renderer;
    
    GameObject inEntity = null;
    public event System.Action<HoleObject> InEvent;

    bool fixedObject = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (inEntity) return;

        bool isJunkFile = other.TryGetComponent<JunkFileObject>(out var junkSys);
        if (!isJunkFile) return; // 정크 파일이 아님
        
        inEntity = other.gameObject;
        InEvent?.Invoke(this);

        
        junkSys.EnableActive();
        StartCoroutine(AttachObject());
    }

    public GameObject GetInEntity() => inEntity;

    private void Update() {
        if (fixedObject && inEntity)
            inEntity.transform.position = fixedPos.position;
    }

    IEnumerator AttachObject() {
        Rigidbody2D rigid = inEntity.GetComponent<Rigidbody2D>();

        while (Vector3.Distance(inEntity.transform.position, fixedPos.position) > 0.1f) {
            Vector3 dir = (fixedPos.position - inEntity.transform.position).normalized;
            rigid.velocity = dir * 10;
            yield return null;
        }

        fixedObject = true;

        // 장착 됨
        _renderer.material.SetColor(COLOR_MAT_ID, checkColor);
        _renderer.material.SetInt(BLINK_MAT_ID, 0);
    }
}
