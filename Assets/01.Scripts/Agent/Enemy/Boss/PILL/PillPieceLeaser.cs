using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillPieceLeaser : MonoBehaviour
{
    [SerializeField] Transform beamTrm;
    [SerializeField] LayerMask catchLayer; // 막히는 옵젝
    [SerializeField] LayerMask catchTarget;
    [SerializeField] Vector2 size;
    [SerializeField] float padding = 2;

    bool active = false;
    bool targetDetect = false;
    public bool IsTargetDetect => targetDetect;

    Vector3 hitPoint;

    private void Update() {
        if (!active) return;
        
        float angle = transform.rotation.eulerAngles.z;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, size, angle, transform.up, 999, catchLayer);

        beamTrm.localScale = new Vector3(size.x, hit.distance, beamTrm.localScale.z);
        beamTrm.localPosition = new Vector3(beamTrm.localPosition.x, (hit.distance / 2f) + padding, beamTrm.localPosition.z);

        RaycastHit2D hit2 = Physics2D.BoxCast(transform.position, size, angle, transform.up, 999, catchLayer | catchTarget);
        targetDetect = hit2.transform && (catchTarget & (1 << hit2.transform.gameObject.layer)) != 0;
        
        hitPoint = hit.point;
    }

    private void OnDrawGizmos() {
        // Gizmos.color = Color.green;
        // Gizmos.DrawWireSphere(hitPoint, 2);
        // Gizmos.color = Color.white;
    }

    public void SetActive(bool _active) {
        targetDetect = false;
        active = _active;
        beamTrm.gameObject.SetActive(active);
    }
}
