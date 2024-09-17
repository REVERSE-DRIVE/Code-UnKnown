using System;
using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using UnityEngine;

public enum PillDirection {
    Left,
    Right
}

public class PillPiece : Enemy
{
    public EnemyStateMachine<PillPieceStateEnum> StateMachine { get; private set; }

    public PillDirection Direction { get; private set; }
    public PillBody Body { get; private set; }

    public float disbandBack = 0.2f; // 해체될때 뒤로 얼마나 뺄건지
    public float disbandDuration = 0.3f; // 해체 뒤로 뺄때 걸리는 시간

    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new();
        SetStateEnum();
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

    }    
}
