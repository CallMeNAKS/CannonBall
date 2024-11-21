using CodeBase.PostEffect;
using UnityEngine;

namespace CodeBase.Domain.Enemy.State
{
    public class FightState : IState
    {
        private IShooter _shooter;
        private IMover _mover;
        private readonly VolumeEffects _volumeEffects;

        public FightState(IShooter shooter, IMover mover, VolumeEffects volumeEffects)
        {
            _shooter = shooter;
            _mover = mover;
            _volumeEffects = volumeEffects;
        }

        public void Enter()
        {
            _volumeEffects.DamageEffect();
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