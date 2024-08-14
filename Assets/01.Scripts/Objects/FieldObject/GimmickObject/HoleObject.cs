using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleObject : MonoBehaviour
{
    [SerializeField] Transform fixedPos;
    
    GameObject inEntity = null;
    public event System.Action<HoleObject> InEvent;

    bool fixedObject = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (inEntity) return;

        bool isJunkFile = other.TryGetComponent<JunkFileObject>(out var junkSys);
        if (!isJunkFile) return; // 정크 파일이 아님
        
        inEntity = other.gameObject;
        InEvent?.Invoke(this);

        Vector3 dir = (fixedPos.position - inEntity.transform.position).normalized;
        Rigidbody2D rigid = inEntity.GetComponent<Rigidbody2D>();
        
        junkSys.EnableActive();
        rigid.velocity = dir * 10;
    
        StartCoroutine(AttachObject());
    }

    public GameObject GetInEntity() => inEntity;

    private void Update() {
        if (fixedObject && inEntity)
            inEntity.transform.position = fixedPos.position;
    }

    IEnumerator AttachObject() {
        yield return new WaitUntil(() => Vector3.Distance(inEntity.transform.position, fixedPos.position) < 0.1f);
        fixedObject = true;
    }
}
