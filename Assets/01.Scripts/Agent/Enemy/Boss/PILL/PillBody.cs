using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBody : MonoBehaviour
{
    [field: Header("Piece Section")]
    [field: SerializeField] public PillPiece LeftPiece { get; private set; }
    [field: SerializeField] public PillPiece RightPiece { get; private set; }

    [Header("Point Section")]
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;

    public PillEquipStatus EquipStatus { get; private set; }
    Transform _targetTrm;

    private void Awake() {
        EquipStatus = new(this);
        _targetTrm = FindAnyObjectByType<Player>().transform;

        LeftPiece.Init(this, _targetTrm, PillDirection.Left);
        RightPiece.Init(this, _targetTrm, PillDirection.Right);

        // TEST
        // StartCoroutine(Testtesttest());
        // TEST END
    }

    IEnumerator Testtesttest() {

        while (true) {
            yield return new WaitForSeconds(10);
            EquipStatus.Start();
            yield return new WaitForSeconds(10);
            LeftPiece.StateMachine.ChangeState(PillPieceStateEnum.Disband);
            RightPiece.StateMachine.ChangeState(PillPieceStateEnum.Disband);
        }
    }

    public Transform GetEquipPoint(PillDirection dir) {
        return dir == PillDirection.Left ? leftPoint : rightPoint;
    }
    public void FinishEquip(PillDirection dir) {
        EquipStatus.Set(dir == PillDirection.Left);
    }
}
