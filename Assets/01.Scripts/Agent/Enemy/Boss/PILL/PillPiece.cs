using System;
using System.Collections;
using System.Collections.Generic;
using EnemyManage;
using UnityEngine;

public class PillPiece : Enemy
{
    public EnemyStateMachine<PillPieceStateEnum> StateMachine { get; private set; }

    PillBody _body;

    protected override void Awake()
    {
        base.Awake();
        
        StateMachine = new();
        SetStateEnum();
    }

    private void Start() {
        StateMachine.Initialize(PillPieceStateEnum.Idle, this);
    }

    private void Update() {
        StateMachine.CurrentState.UpdateState();
    }

    public void Init(PillBody body, Transform target) {
        _body = body;
        targetTrm = target;
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
