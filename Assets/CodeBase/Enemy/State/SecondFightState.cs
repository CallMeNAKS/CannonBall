using UnityEngine;

namespace CodeBase.Domain.Enemy.State
{
    public class SecondFightState : IState
    {
        private readonly IShooter _shooter;
        private readonly IMover _mover;

        public SecondFightState(IShooter shooter, IMover mover)
        {
            _shooter = shooter;
            _mover = mover;
        }

        public void Enter()
        {
            Debug.Log("Enter SecondFightState");
            Execute();
        }

        public void Execute()
        {
            _shooter.StartShootingSecond();
            _mover.Move();
        }

        public void Exit()
        {
            _mover.StopMoving();
            _shooter.StopShooting();
            Debug.Log("Exit SecondFightStage");
        }
    }
}