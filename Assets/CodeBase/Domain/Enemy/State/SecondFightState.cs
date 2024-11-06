using UnityEngine;

namespace CodeBase.Domain.Enemy.State
{
    public class SecondFightState : IState
    {
        private IShooter _shooter;

        public SecondFightState(IShooter shooter)
        {
            _shooter = shooter;
        }

        public void Enter()
        {
            Debug.Log("Enter SecondFightState");
            Execute();
        }

        public void Execute()
        {
            _shooter.StartShootingSecond();
        }

        public void Exit()
        {
            _shooter.StopShooting();
            Debug.Log("Exit SecondFightStage");
        }
    }
}