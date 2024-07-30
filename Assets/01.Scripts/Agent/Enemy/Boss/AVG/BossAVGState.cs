using EnemyManage;

namespace EnemyManage
{
    public class BossAVGState : EnemyState<AVGStateEnum>
    {
        protected AVerG _bossAVGBase;
        public BossAVGState(Enemy enemyBase, EnemyStateMachine<AVGStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _bossAVGBase = (AVerG)_enemyBase;
        }

        public virtual void CustomTrigger()
        {
        
        }
    }
}