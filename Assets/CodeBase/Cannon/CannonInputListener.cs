using System;
using CodeBase.Domain.PlayerInput;
using UnityEngine;

namespace CodeBase.Domain.Cannon
{
    public class CannonInputListener : AbstractCannon
    {
        [SerializeField] private AbstractCannon _cannon;

        private IPlayerInput _playerInput;
        public override void Shoot()
        {
            _cannon.Shoot();
        }

        public override void RocketShoot()
        {
            _cannon.RocketShoot();
        }

        public void SunscribePlayerInput(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.Shooted += Shoot;
            _playerInput.RocketLaunced += RocketShoot;
        }

        private void OnDisable()
        {
            _playerInput.Shooted -= Shoot;
            _playerInput.RocketLaunced -= RocketShoot;
        }
    }
}