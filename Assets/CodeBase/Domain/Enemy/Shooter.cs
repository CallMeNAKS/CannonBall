using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Target;
using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public class Shooter : IShooter
    {
        private Transform[] _projectilesPositions;
        private Transform _target;
        private AbstractProjectilesSource _projectileSource;
        private float _attackPower;
        private float _delay;

        private bool _isShooting;

        public Shooter(Transform[] projectilesPositions, Transform target, AbstractProjectilesSource projectileSource, float attackPower, float delay)
        {
            _projectilesPositions = projectilesPositions;
            _target = target;
            _projectileSource = projectileSource;
            _attackPower = attackPower;
            _delay = delay;
        }

        public async Task StartShooting()
        {
            _isShooting = true;
            
            while (_isShooting)
            {
                foreach (var position in _projectilesPositions)
                {
                    position.LookAt(_target.position);
                    Vector3 direction = _target.position + new Vector3(0, 1f, 0) - position.position;
                    AbstractProjectile projectile = _projectileSource.GetTarget();
                    projectile.transform.position = position.position;
                    projectile.ApplyPower(direction * _attackPower);
                    
                    await Task.Delay(TimeSpan.FromSeconds(_delay));
                    if(_isShooting == false) break;
                }
            }
        }

        public async Task StartShootingSecond()
        {
            Debug.Log("Ну прям жесткий растрел");
        }

        public void StopShooting()
        {
            _isShooting = false;
        }
    }
}