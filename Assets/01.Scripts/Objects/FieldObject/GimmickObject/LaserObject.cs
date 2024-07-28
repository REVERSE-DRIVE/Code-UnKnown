using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour
{
    public enum Type { Red, Green }
    [field: SerializeField] public Type LaserType { get; private set; }

    [SerializeField] Transform beamCenter;
    [SerializeField] Transform lookAt;

    [SerializeField] Vector2 beamSize = Vector2.one;
    [SerializeField] float padding = 1;

    [SerializeField] LayerMask filter;

    Transform beamTrm;
    ILaserEvent hitObj;
    
    private void Awake() {
        beamTrm = beamCenter.GetChild(0);
    }

    private void Update() {
        if (lookAt) {
            Vector3 diff = lookAt.position - transform.position;
            beamCenter.up = diff.normalized;
        }

        float angle = Vector2.SignedAngle(transform.up, beamCenter.up);
        RaycastHit2D result = Physics2D.BoxCast(beamCenter.position, beamSize, angle, beamCenter.up, 9999, filter);

        float distance = 0;
        if (result.transform)
            distance = Vector2.Distance(beamCenter.position, result.point) + padding;

        beamTrm.localScale = new Vector2(beamSize.x, distance);

        Vector3 origin = beamTrm.localPosition;
        origin.y = distance / 2f;
        beamTrm.localPosition = origin;

        // 이벤트
        ILaserEvent newEvent = null;
        if (result.transform && result.transform.TryGetComponent<ILaserEvent>(out var evt)) {
            newEvent = evt;
        }

        if (newEvent != hitObj) {
            hitObj?.OnLaserOut(this);
            newEvent?.OnLaserIn(this);

            hitObj = newEvent;
        }
    }
    
    public void ForceHit(ILaserEvent obj) {
        hitObj = obj;
    }

    public void Init(Type type, Transform look) {
        LaserType = type;
        lookAt = look;
    }
}
