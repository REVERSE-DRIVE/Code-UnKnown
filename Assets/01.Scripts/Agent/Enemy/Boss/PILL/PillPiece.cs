using System;
using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using Unity.VisualScripting;
using UnityEngine;

public enum PillDirection : byte {
    Left,
    Right
}

public class PillPiece : Enemy
{
    public EnemyStateMachine<PillPieceStateEnum> StateMachine { get; private set; }

    public PillDirection Direction { get; private set; }
    public PillBody Body { get; private set; }
    [field: SerializeField] public PillPieceLeaser Leaser { get; private set; }


    public float disbandBack = 0.2f; // 해체될때 뒤로 얼마나 뺄건지
    public float disbandDuration = 0.3f; // 해체 뒤로 뺄때 걸리는 시간
    public float rushCooltime = 6f;
    public float rushDuration = 1.5f; // 3초간 돌진~~~
    public int rushSpeed = 8; // 돌진할때 속도
    public int rushDamage = 10;
    public float lookSpeed = 10;
    public float healBulletFireCooltime = 1f; // 치유액 발사 대기시간
    public CureProjectile cureBulletPrefab;
    public Transform firePos;
    public int leaserDamage = 5;
    public float leaserCooltime = 1f; // 레이저로 맞고 나서 무시 되는 시간

    protected override void Awake()
    {
        base.Awake();

        HealthCompo.OnHealthChangedValueEvent += HandleHealthChanged;
        
        StateMachine = new();
        SetStateEnum();
    }

    private void HandleHealthChanged(int prevValue, int newValue, int max)
    {
        int amount = prevValue - newValue;
        if (amount <= 0) return; // 이건 힐 한거 아님??
        
        HealthCompo.RestoreHealth(amount); // 자신 체력은 닳지 않게
        Body.TakeHit(Direction, amount);
    }

    private void Start() {
        StateMachine.Initialize(PillPieceStateEnum.Disband, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    public void Init(PillBody body, Transform target, PillDirection dir) {
        Body = body;
        targetTrm = target;
        Direction = dir;
    }

    protected void SetStateEnum()
    {
        foreach (PillPieceStateEnum stateEnum in Enum.GetValues(typeof(PillPieceStateEnum)))
        {
            string typeName = stateEnum.ToString();
            Type t = Type.GetType($"EnemyManage.BossPillPiece{typeName}State");

            try
            {
                EnemyState<PillPieceStateEnum> state =  Activator.CreateInstance(t, this, StateMachine, $"State{typeName}") as EnemyState<PillPieceStateEnum>;
                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Enemy Boss PILLPIECE : no State found [ {typeName} ] - {ex.Message}");
            }
        }
    }

    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }    
    
    public void SetHighlight(bool active) {
        _spriteRenderer.color = active ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1);
    }
}
