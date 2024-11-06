using System;
using System.Collections.Generic;
using System.Threading;
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

        private CancellationTokenSource _cancellationTokenSource;

        public Shooter(Transform[] projectilesPositions, Transform target, AbstractProjectilesSource projectileSource, float attackPower, float delay)
        {
            _projectilesPositions = projectilesPositions;
            _target = target;
            _projectileSource = projectileSource;
            _attackPower = attackPower;
            _delay = delay;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartShooting()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            while (!token.IsCancellationRequested)
            {
                foreach (var position in _projectilesPositions)
                {
                    if (token.IsCancellationRequested) break;

                    Vector3 direction = _target.position + new Vector3(0, 1f, 0) - position.position;
                    AbstractProjectile projectile = _projectileSource.GetTarget();
                    projectile.transform.position = position.position;
                    projectile.ApplyPower(direction * _attackPower);

                    await Task.Delay(TimeSpan.FromSeconds(_delay), token);
                }
            }
        }

        public async Task StartShootingSecond()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            while (!token.IsCancellationRequested)
            {
                foreach (var position in _projectilesPositions)
                {
                    if (token.IsCancellationRequested) break;

                    Vector3 direction = _target.position + new Vector3(0, 1f, 0) - position.position;
                    AbstractProjectile projectile = _projectileSource.GetTarget();
                    projectile.transform.position = position.position;
                    projectile.ApplyPower(direction * _attackPower);

                    await Task.Delay(TimeSpan.FromSeconds(_delay / 2), token);
                }
            }
        }

        public void StopShooting()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
