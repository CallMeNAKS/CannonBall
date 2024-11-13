using CodeBase.Domain.Enemy.State;
using DG.Tweening;

namespace CodeBase.Domain.Enemy
{
    public class BaseEnemy : AbstractEnemy
    {
        private IStateMachine _stateController; // поменять
        private EnemyState _currentState; // убрать 

        private void OnEnable()
        {
            HealthComponent.HealthReducedHalf += StartSecondState;
            HealthComponent.HealthEnded += StartLooseState;
        }

        private void StartSecondState()
        {
            ChangeState(EnemyState.SecondFight);
        }

        private void StartLooseState()
        {
            ChangeState(EnemyState.Loose);
            Death();
        }

        public override void Reset()
        {
            StopAllCoroutines();
            transform.DOKill();
            Shooter.StopShooting();
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

            _stateController.Enter(EnemyState.Fight);
        }

        private void RegisterStates()
        {
            var startState = new StartState();
            var fightState = new FightState(Shooter, Mover);
            var secondFightState = new SecondFightState(Shooter, Mover);
            var looseStage = new LooseStage();

            _stateController.RegisterState(EnemyState.Start, startState);
            _stateController.RegisterState(EnemyState.Fight, fightState);
            _stateController.RegisterState(EnemyState.SecondFight, secondFightState);
            _stateController.RegisterState(EnemyState.Loose, looseStage);
        }

        private void OnDisable()
        {
            HealthComponent.HealthReducedHalf -= StartSecondState;
            HealthComponent.HealthEnded -= StartLooseState;
        }
    }
}