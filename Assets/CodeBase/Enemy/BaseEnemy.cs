using System;
using CodeBase.Domain.Enemy.State;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public class BaseEnemy : AbstractEnemy
    {
        private void OnEnable()
        {
            HealthComponent.HealthReducedHalf += StartSecondState;
            HealthComponent.HealthEnded += StartLooseState;
            
            // StateMachine.Enter(EnemyState.Start);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.DOScale(transform.localScale * 4f, 15f);
            }
        }
        
        public override void StartIdleState()
        {
            StateMachine.Enter(EnemyState.Start);
        }

        public override void StartFighting()
        {
            StateMachine.Enter(EnemyState.Fight);
        }

        private void StartSecondState()
        {
            StateMachine.Enter(EnemyState.SecondFight);
        }

        private void StartLooseState()
        {
            StateMachine.Enter(EnemyState.Loose);
            Death();
        }

        public override void Reset()
        {
            StopAllCoroutines();
            transform.DOKill();
            Shooter.StopShooting();
        }
        
        private void OnDisable()
        {
            HealthComponent.HealthReducedHalf -= StartSecondState;
            HealthComponent.HealthEnded -= StartLooseState;
        }
    }
}