using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBody : Boss
{
    [field: Header("Piece Section")]
    [field: SerializeField] public PillPiece LeftPiece { get; private set; }
    [field: SerializeField] public PillPiece RightPiece { get; private set; }

    [Header("Point Section")]
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;

    public PillEquipStatus EquipStatus { get; private set; }
    Transform _targetTrm;
    IDamageable targetDamageable;
    PillDirection hitEnemyDir; // 때려야하는 애

    // boss지만 다른건 필요 업음
    private new void Awake() {
        // 체력만 씀
        HealthCompo = GetComponent<Health>();
        HealthCompo.Initialize(this);

        EquipStatus = new(this);
        _targetTrm = FindAnyObjectByType<Player>().transform;
        targetDamageable = _targetTrm.GetComponent<IDamageable>();

        LeftPiece.Init(this, _targetTrm, PillDirection.Left);
        RightPiece.Init(this, _targetTrm, PillDirection.Right);

        // TEST
        // StartCoroutine(Testtesttest());
        // TEST END
    }

    private void Start() {
        hitEnemyDir = (PillDirection)Random.Range(0, 2);
        HighlighttUpdate();
    }

    IEnumerator Testtesttest() {

        // while (true) {
            yield return new WaitForSeconds(5);
            LeftPiece.StateMachine.ChangeState(PillPieceStateEnum.CureAttack);
            RightPiece.StateMachine.ChangeState(PillPieceStateEnum.CureAttack);

            // EquipStatus.Start();
            // yield return new WaitForSeconds(10);
            // LeftPiece.StateMachine.ChangeState(PillPieceStateEnum.Disband);
            // RightPiece.StateMachine.ChangeState(PillPieceStateEnum.Disband);
        // }
    }

    public Transform GetEquipPoint(PillDirection dir) {
        return dir == PillDirection.Left ? leftPoint : rightPoint;
    }
    public void FinishEquip(PillDirection dir) {
        EquipStatus.Set(dir == PillDirection.Left);
    }

    void HighlighttUpdate() {
        LeftPiece.SetHighlight(hitEnemyDir == PillDirection.Left);
        RightPiece.SetHighlight(hitEnemyDir == PillDirection.Right);
    }

    // 자식이 쳐맞음
    public void TakeHit(PillDirection dir, int amount) {
        print($"TakeHit {dir}");
        // 왼쪽 때려야하는지 오른쪽 때려야 하는지 체크
        if (hitEnemyDir != dir) {
            // 어따 떄림??
            targetDamageable.TakeDamage(amount / 2);
            return;
        }

        hitEnemyDir = (PillDirection)((byte)hitEnemyDir == 0 ? 1 : 0);
        HealthCompo.TakeDamage(amount);

        HighlighttUpdate();
    }
}
