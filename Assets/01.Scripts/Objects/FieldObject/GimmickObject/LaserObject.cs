using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LaserDirection {
    Top = 1,
    Bottom = 2,
    Left = 4,
    Right = 8
}

public class LaserObject : MonoBehaviour
{
    public enum Type { Red, Green }
    [field: SerializeField] public Type LaserType { get; private set; }

    [SerializeField] Transform beamCenter;
    [SerializeField] Transform lookAt;

    [SerializeField] Vector2 beamSize = Vector2.one;
    [SerializeField] float padding = 1;

    [Header("Move Setting")]
    [SerializeField] bool activeMove = false;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float moveDistance = 1.5f;
    [SerializeField] float moveDelay = 3f;

    [SerializeField] LayerMask filter;

    RaycastHit2D[] beamResults = new RaycastHit2D[2];
    Transform beamTrm;
    ILaserEvent hitObj;

    Vector3 testStartPos;

    // move
    byte possibleDir;

    public event System.Action OnRemove;
    
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

        if (activeMove)
            StartCoroutine(WaitDomi());

        testStartPos = transform.position;
    }

    IEnumerator WaitDomi() {
        yield return new WaitForSeconds(1);
        possibleDir = GetPossibleDir();
    }

    private void Update() {
        LookAt();
        if (activeMove)
            Move();

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

    byte alreadyMoveDir = 0;
    byte moveDir = 0;
    float moveTime = 0;
    Vector3 startMovePos;
    float moveWait = 5;
    List<LaserDirection> moveDirectionsAlloc = new();

    private void Move()
    {
        if (possibleDir == 0) return; // 움직일수가 없음

        
        if (moveWait > 0) {
            moveWait -= Time.deltaTime;
            return;
        }

        if (moveDir == 0) { // 어디로 움직일지 정하기
            moveDir = (byte)GetRandomMoveDir();
            byte reverseFlag = (byte)ReverseD_irection((LaserDirection)moveDir);
            if ((alreadyMoveDir & reverseFlag)> 0) { // 반대로 이미 움직인 상태면 지움
                alreadyMoveDir &= (byte)~reverseFlag;
            }

            alreadyMoveDir |= moveDir;

            moveTime = 0;
            startMovePos = transform.position;
        }

        moveTime += Time.deltaTime / moveSpeed;
        transform.position = Vector3.Lerp(startMovePos, startMovePos + (GetMoveVectorDirection((LaserDirection)moveDir) * moveDistance), moveTime);
        // transform.position = Vector3.MoveTowards(startMovePos, startMovePos + (GetMoveVectorDirection((LaserDirection)moveDir) * moveDistance), moveTime);
        // transform.position = startMovePos + (GetMoveVectorDirection((LaserDirection)moveDir) * moveDistance);

        if (moveTime >= 1) {
            moveDir = 0;
            moveWait = moveDelay;
        }
    }

    LaserDirection GetRandomMoveDir() {
        List<LaserDirection> list = new();

        // 가능한거
        GetMoveDirections(possibleDir);
        foreach (var item in moveDirectionsAlloc)
            list.Add(item);

        // 불가능 한거 삭제 (이미 움직인 거)
        GetMoveDirections(alreadyMoveDir);
        foreach (var item in moveDirectionsAlloc)
            list.Remove(item);

        // if (list.Count == 0) { // 할수있는게 없음 (이미 움직인거 반대로)
            foreach (var item in moveDirectionsAlloc)
                list.Add(ReverseD_irection(item));
        // }
        
        if (list.Count == 0) throw new System.Exception("GetRandomMoveDir list 비어있음!!");
        return list[Random.Range(0, list.Count)];
    }


    void GetMoveDirections(byte flag) {
        moveDirectionsAlloc.Clear();

        if ((flag & (byte)LaserDirection.Top) > 0)
            moveDirectionsAlloc.Add(LaserDirection.Top);
            
        if ((flag & (byte)LaserDirection.Left) > 0)
            moveDirectionsAlloc.Add(LaserDirection.Left);
            
        if ((flag & (byte)LaserDirection.Right) > 0)
            moveDirectionsAlloc.Add(LaserDirection.Right);
            
        if ((flag & (byte)LaserDirection.Bottom) > 0)
            moveDirectionsAlloc.Add(LaserDirection.Bottom);

    }

    LaserDirection ReverseD_irection(LaserDirection dir) {
        switch (dir)
        {
            case LaserDirection.Top:
                return LaserDirection.Bottom;
            case LaserDirection.Bottom:
                return LaserDirection.Top;
            case LaserDirection.Left:
                return LaserDirection.Right;
            case LaserDirection.Right:
                return LaserDirection.Left;
            default:
                return LaserDirection.Top;
        }
    }

    Vector3 GetMoveVectorDirection(LaserDirection dir) {
        switch (dir)
        {
            case LaserDirection.Top:
                return Vector3.up;
            case LaserDirection.Bottom:
                return -Vector3.up;
            case LaserDirection.Left:
                return Vector3.left;
            case LaserDirection.Right:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }
    

    public byte GetPossibleDir() {
        byte result = 0;
        if (RayResult(transform.position, Vector2.up, moveDistance).transform == null) result |= (byte)LaserDirection.Top;
        if (RayResult(transform.position, Vector2.down, moveDistance).transform == null) result |= (byte)LaserDirection.Bottom;
        if (RayResult(transform.position, Vector2.left, moveDistance).transform == null) result |= (byte)LaserDirection.Left;
        if (RayResult(transform.position, Vector2.right, moveDistance).transform == null) result |= (byte)LaserDirection.Right;

        return result;
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
            if (item.transform == null || item.transform != transform) return item;
        }
        
        return default;
    }

    RaycastHit2D RayResult(Vector3 pos, Vector3 dir, float distance) {
        int result = Physics2D.RaycastNonAlloc(pos, dir, beamResults, distance + padding, filter);
        
        if (result == 0) return default;
        
        for (int i = 0; i < result; i++)
        {
            RaycastHit2D data = beamResults[i];
            if (data.transform == null || data.transform != transform) return data;
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

    public void SetMove(bool active) => activeMove = active;

    private void OnEnable() {
        if (beamTrm)
            beamTrm.gameObject.SetActive(true);
    }

    private void OnDisable() {
        if (beamTrm)
            beamTrm.gameObject.SetActive(false);
    }

    private void OnDestroy() {

    }

    private void OnCollisionEnter2D(Collision2D other) => DestoryCheck(other);
    private void OnCollisionStay2D(Collision2D other) => DestoryCheck(other);

    void DestoryCheck(Collision2D other) {
        if (!other.gameObject.TryGetComponent<JunkFileObject>(out var junkSys)) return;
        if (!junkSys.GetActive()) return;
        
        enabled = false;
        OnRemove?.Invoke();
        hitObj?.OnLaserOut(this);

        Destroy(gameObject);
    }
}
