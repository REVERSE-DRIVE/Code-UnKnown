using System;
using ObjectPooling;
using UnityEngine;

namespace EnemyManage
{

    public class AVerG : Boss
    {
        public EnemyStateMachine<AVGStateEnum> StateMachine { get; private set; }

        #region Settings

        //[SerializeField] internal SoundObject _soundObject;
        [Header("Idle State Setting")] [SerializeField]
        internal AVGStateEnum[] _randomPickState;

        [SerializeField] internal float _idleWaitingTime = 5f;

        [Header("Stun State Setting")] [SerializeField]
        internal float _stunDuration = 5;

        [Header("Red State Setting")] 
        [SerializeField] internal int _chargeEnergy = 20;

        [SerializeField] internal int _burstDamage = 100;
        [SerializeField] internal float _chargingSpeed = 2;
        [SerializeField] internal AVGStructureObject _structureObject;
        [SerializeField] internal ParticleSystem _chargingParticle;
        [SerializeField] internal ParticleSystem _burstParticle;

        [Header("Green State Setting")] [SerializeField]
        internal float _greenStateDuration = 30f;

        [SerializeField] internal int _healCoreHealAmountPerSecond = 30;

        [SerializeField] internal AVGHealingObject[] _healingObjects;

        //[SerializeField] private int _healMultiply = 3;
        [Header("Blue State Setting")] [SerializeField]
        internal float _attacktime = 10f;

        [SerializeField] internal float _attackCooltime = 10f;
        [SerializeField] internal PoolingType _projectile;
        [SerializeField] internal int _fireProjectileAmount = 4;
        [SerializeField] internal float _rotationSpeed = 3f;

        [Header("Yellow State Setting")] [SerializeField]
        internal float _yellowStateDuration = 30f;

        [SerializeField] internal bool _isResist;
        
        
        #endregion


        protected override void Awake()
        {
            base.Awake();
            StateMachine = new EnemyStateMachine<AVGStateEnum>();
            //_soundObject = GetComponent<SoundObject>();
            //여기에 상태를 불러오는 코드가 필요하다.
            SetStateEnum();

        }

        protected void SetStateEnum()
        {
            foreach (AVGStateEnum stateEnum in Enum.GetValues(typeof(AVGStateEnum)))
            {
                string typeName = stateEnum.ToString();
                Type t = Type.GetType($"EnemyManage.BossAVG{typeName}State");

                try
                {
                    EnemyState<AVGStateEnum> state =
                        Activator.CreateInstance(t, this, StateMachine, $"State{typeName}") as EnemyState<AVGStateEnum>;
                    StateMachine.AddState(stateEnum, state);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Enemy Boss AVG : no State found [ {typeName} ] - {ex.Message}");
                }
            }
        }

        private void Start()
        {
            StateMachine.Initialize(AVGStateEnum.Idle, this);
        }



        private void Update()
        {
            StateMachine.CurrentState.UpdateState();
        }

        public void ForceStun()
        {
            StateMachine.ChangeState(AVGStateEnum.Stun, true);

        }


        public void TakeStrongDamage(int amount)
        {
            print("AVG Stun");
            if (_isResist)
            {
                StateMachine.CurrentState.CustomTrigger();
                _isResist = false;
            }
        }


        public void Attack()
        {
        }

        public override void AnimationEndTrigger()
        {
            StateMachine.CurrentState.AnimationTrigger();
        }



        public void OnHealDefense()
        {
            throw new NotImplementedException();
        }
    }
}