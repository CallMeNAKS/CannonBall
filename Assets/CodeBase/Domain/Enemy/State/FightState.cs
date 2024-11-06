using UnityEngine;

namespace CodeBase.Domain.Enemy.State
{
    public class FightState : IState
    {
        private IShooter _shooter;

        public FightState(IShooter shooter)
        {
            _shooter = shooter;
        }

        public void Enter()
        {
            Debug.Log("Enter FightState");
            Execute();
        }

        public void Execute()
        {
            _shooter.StartShooting();
        }

        public void Exit()
        {
            _shooter.StopShooting();
            Debug.Log("Exit FightState");
        }
    }
}