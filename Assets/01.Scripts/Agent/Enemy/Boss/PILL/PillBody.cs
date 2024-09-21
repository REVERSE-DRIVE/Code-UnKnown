using System;
using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using UnityEngine;

public class PillBody : Boss
{
    public EnemyStateMachine<PillBodyStateEnum> StateMachine { get; private set; }

    [field: Header("Piece Section")]
    [field: SerializeField] public PillPiece LeftPiece { get; private set; }
    [field: SerializeField] public PillPiece RightPiece { get; private set; }

    [Header("Point Section")]
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;

    [Header("ShakeWaveSkill Section")]
    public float shockWaveRadius = 10f;
    public int shockWaveDamage = 5;
    public float shockWaveWait = 0.3f;
    public GameObject shockWavePrefab;

    [Header("LaserRotate Section")]
    public float laserRotateDuration = 5f;
    public float laserRotateSpeed = 10f;
    public float laserCamShakePower = 3f;

    [Header("Skill Section")]
    public Vector2 skillUseTime = new Vector2(5, 25);
    public PillBodyStateEnum[] useSkills;
    public PillPieceStateEnum[] usePieceSkills;

    public PillEquipStatus EquipStatus { get; private set; }
    Transform _targetTrm;
    IDamageable targetDamageable;
    PillDirection hitEnemyDir; // 때려야하는 애

    // boss지만 다른건 필요 업음
    private new void Awake() {
        // 체력만 씀
        HealthCompo = GetComponent<Health>();
        HealthCompo.Initialize(this);

        AnimatorCompo = GetComponent<Animator>();

        EquipStatus = new(this);
        _targetTrm = FindAnyObjectByType<Player>().transform;
        targetDamageable = _targetTrm.GetComponent<IDamageable>();

        LeftPiece.Init(this, _targetTrm, PillDirection.Left);
        RightPiece.Init(this, _targetTrm, PillDirection.Right);
        
        StateMachine = new();
        SetStateEnum();

        // TEST
        // StartCoroutine(Testtesttest());
        // TEST END
    }

    private void Start() {
        hitEnemyDir = (PillDirection)UnityEngine.Random.Range(0, 2);
        HighlighttUpdate();

        StateMachine.Initialize(PillBodyStateEnum.Idle, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    protected void SetStateEnum(){
        foreach (PillBodyStateEnum stateEnum in Enum.GetValues(typeof(PillBodyStateEnum)))
        {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"EnemyManage.BossPillBody{typeName}State");

            try
            {
                EnemyState<PillBodyStateEnum> state =  Activator.CreateInstance(t, this, StateMachine, $"State{typeName}") as EnemyState<PillBodyStateEnum>;
                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Enemy Boss PILLBODY : no State found [ {typeName} ] - {ex.Message}");
            }
        }
    }

    IEnumerator Testtesttest() {

        // while (true) {
            yield return new WaitForSeconds(5);
            StateMachine.ChangeState(PillBodyStateEnum.LaserAttack);
            // LeftPiece.StateMachine.ChangeState(PillPieceStateEnum.CureAttack);
            // RightPiece.StateMachine.ChangeState(PillPieceStateEnum.CureAttack);

            // yield return new WaitForSeconds(5);
            // StateMachine.ChangeState(PillBodyStateEnum.ShockWave);

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

    public void AllChangeState(PillPieceStateEnum e) {
        LeftPiece.StateMachine.ChangeState(e);
        RightPiece.StateMachine.ChangeState(e);
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
