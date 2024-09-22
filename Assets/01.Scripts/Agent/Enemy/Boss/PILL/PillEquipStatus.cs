public class PillEquipStatus {
    PillBody body;
    bool process = false;

    bool left = false;
    bool right = false;

    public PillEquipStatus(PillBody _body) {
        body = _body;
    }

    public void Clear() {
        process = left = right = false;
    }

    public void Start() {
        process = true;

        body.LeftPiece.StateMachine.ChangeState(PillPieceStateEnum.Equip);
        body.RightPiece.StateMachine.ChangeState(PillPieceStateEnum.Equip);
    }

    public void Set(bool isLeft) {
        if (isLeft)
            left = true;
        else
            right = true;
    }

    public bool IsSuccess() => left && right;
}