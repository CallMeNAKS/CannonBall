using System;
using CodeBase.Domain.Enemy.State;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Domain.Enemy
{
    public class BaseEnemy : AbstractEnemy
    {
        [SerializeField] private float _timeToChangePlace = 2f;

        private Vector3 _startPosition;
        private IStateMachine _stateController;

        private EnemyState _currentState;

        public override event Action<float> DamageTaken;

        public override void CreateShooter()
        {
            Shooter = new Shooter(ProjectilesPositions, Target, ProjectilesSource, AttackPower, FireRate);
        }

        public override void Move()
        {
            _startPosition = transform.position;
            MoveToRandomPosition();
        }

        public override void Reset()
        {
            StopAllCoroutines();
            transform.DOKill();
            transform.position = _startPosition;
            Shooter.StopShooting();
        }

        private void MoveToRandomPosition()
        {
            float randomX = Random.Range(-10f, 10f);
            float randomY = Random.Range(3f, 10f);

            Vector3 targetPosition = new Vector3(randomX, randomY, transform.position.z);

            transform.DOMove(targetPosition, _timeToChangePlace).OnComplete(MoveToRandomPosition);
        }

        public override void TakeDamage(float damage)
        {
            if (Health > 0)
            {
                Health -= damage;
                
                if (Health > MaxHealth / 2)
                {
                    ChangeState(EnemyState.Fight);
                }
                else
                {
                    ChangeState(EnemyState.SecondFight);
                }

                DamageTaken?.Invoke(Health);
            }

            if (Health <= 0)
            {
                ChangeState(EnemyState.Loose);
                Death();
            }
        }

        private void ChangeState(EnemyState state)
        {
            if (state == _currentState)
                return;
            _stateController.Enter(state);
            _currentState = state;
        }

        public override void CreateStateMachine()
        {
            _stateController = new EnemyStateController();

            RegisterStates();

            _stateController.Enter(EnemyState.Start);
        }

        private void RegisterStates()
        {
            var startState = new StartState();
            var fightState = new FightState(Shooter);
            var secondFightState = new SecondFightState(Shooter);
            var looseStage = new LooseStage();

            _stateController.RegisterState(EnemyState.Start, startState);
            _stateController.RegisterState(EnemyState.Fight, fightState);
            _stateController.RegisterState(EnemyState.SecondFight, secondFightState);
            _stateController.RegisterState(EnemyState.Loose, looseStage);
        }
    }
}