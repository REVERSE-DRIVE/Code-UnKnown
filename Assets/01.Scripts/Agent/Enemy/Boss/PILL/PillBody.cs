using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBody : MonoBehaviour
{
    [SerializeField] PillPiece leftPiece;
    [SerializeField] PillPiece rightPiece;

    Transform _targetTrm;

    private void Awake() {
        _targetTrm = FindAnyObjectByType<Player>().transform;

        leftPiece.Init(this, _targetTrm, PillDirection.Left);
        rightPiece.Init(this, _targetTrm, PillDirection.Right);
    }
}
