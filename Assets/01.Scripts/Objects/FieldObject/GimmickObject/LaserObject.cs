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

    RaycastHit2D[] beamResults = new RaycastHit2D[2];
    Transform beamTrm;
    ILaserEvent hitObj;
    
    private void Awake() {
        beamTrm = beamCenter.GetChild(0);
    }

    private void Start() {
        SpriteRenderer render = beamTrm.GetComponent<SpriteRenderer>();
        
        Color color = Color.black;
        switch (LaserType)
        {
            case Type.Red:
                color = Color.red;
                break;
            case Type.Green:
                color = Color.green;
                break;
        }
        color.a = 0.8f;

        render.color = color;
    }

    private void Update() {
        LookAt();

        RaycastHit2D result = Beam();

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

    public void LookAt() {
        if (lookAt) {
            Vector3 diff = lookAt.position - transform.position;
            beamCenter.up = diff.normalized;
        }
    }
    
    public RaycastHit2D Beam() {
        float angle = Vector2.SignedAngle(transform.up, beamCenter.up);
        Physics2D.BoxCastNonAlloc(beamCenter.position, beamSize, angle, beamCenter.up, beamResults, 9999, filter);

        foreach (var item in beamResults) {
            if (item.transform == null || item.transform.parent != transform) return item;
        }
        
        return default;
    }

    public void ForceHit(ILaserEvent obj) {
        hitObj = obj;
    }

    public void Init(Type type, Transform look) {
        LaserType = type;
        lookAt = look;
    }
}
