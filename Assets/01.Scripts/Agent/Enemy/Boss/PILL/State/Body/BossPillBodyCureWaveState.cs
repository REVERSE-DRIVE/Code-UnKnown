using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnemyManage;
using UnityEngine;

namespace EnemyManage {
    public class BossPillBodyCureWaveState : EnemyState<PillBodyStateEnum>
    {
        PillBody agent;
        float timer;
        IEnumerator process;

        public BossPillBodyCureWaveState(Enemy enemyBase, EnemyStateMachine<PillBodyStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
        {
            agent = enemyBase as PillBody;
        }

        public override void Enter()
        {
            base.Enter();
            
            // 모여
            agent.EquipStatus.Start();

            timer = 0;

            process = HandleCoroutine();
            agent.StartCoroutine(process);
        }

        IEnumerator HandleCoroutine() {
            yield return new WaitUntil(() => agent.EquipStatus.IsSuccess()); // 합쳐질때까지 기달

            var effect = GameObject.Instantiate(agent.cureWavePrefab, agent.transform.position, Quaternion.identity);
            SpriteRenderer effectVisual = effect.transform.Find("Visual").GetComponent<SpriteRenderer>();

            effectVisual.transform.localScale = new Vector3(agent.cureWaveRadius * 2, agent.cureWaveRadius * 2, 1);
            
            Color basicColor = effectVisual.color;
            effectVisual.color = new Color(basicColor.r, basicColor.g, basicColor.b, 0);
            
            ShockWaveObject effectSys = effect.GetComponent<ShockWaveObject>();
            effectSys.enabled = false; // 일단 끔

            var tween = effectVisual.DOFade(basicColor.a, agent.cureWaveWait).SetEase(Ease.Linear);
            yield return tween.WaitForCompletion(); // 다 될때까지 ㄱㄷ

            agent.DamageCasterCompo.CastDamage(agent.cureWaveDamage, agent.cureWaveRadius);
            effectSys.enabled = true;
            CameraManager.Instance.Shake(10, 1);

            agent.LeftPiece.Stat.moveSpeed.AddModifier((int)(agent.LeftPiece.Stat.moveSpeed.GetValue() * agent.cureWaveSkillMoveUp));
            agent.RightPiece.Stat.moveSpeed.AddModifier((int)(agent.RightPiece.Stat.moveSpeed.GetValue() * agent.cureWaveSkillMoveUp));

            _stateMachine.ChangeState(PillBodyStateEnum.Idle);
        }

        public override void Exit()
        {
            base.Exit();
            agent.EquipStatus.Clear();
            agent.AllChangeState(PillPieceStateEnum.Disband);
        
            if (process != null) {
                agent.StopCoroutine(process);
                process = null;
            }
        }
    }
}