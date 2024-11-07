using UnityEngine;

namespace CodeBase.Domain.Enemy.State
{
    public class FightState : IState
    {
        private IShooter _shooter;
        private IMover _mover;

        public FightState(IShooter shooter, IMover mover)
        {
            _shooter = shooter;
            _mover = mover;
        }

        public void Enter()
        {
            Debug.Log("Enter FightState");
            Execute();
        }

        public void Execute()
        {
            _shooter.StartShooting();
            _mover.Move();
        }

        public void Exit()
        {
            _mover.StopMoving();
            _shooter.StopShooting();
            Debug.Log("Exit FightState");
        }
    }
}