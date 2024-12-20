﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Target;
using Domain.Target.Source;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Domain.Enemy
{
    public class Shooter : MonoBehaviour, IShooter
    {
        [SerializeField] private Transform[] _projectilesPositions;
        [SerializeField] private float _attackPower;
        [SerializeField] private float _fireDelay = 0.5f;

        private Transform _target;
        private ProjectilesSource _projectileSource;

        private CancellationTokenSource _cancellationTokenSource;

        public void Init(Transform target, ProjectilesSource projectileSource)
        {
            _target = target;
            _projectileSource = projectileSource;
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
                    AbstractProjectile projectile = _projectileSource.Get();
                    projectile.transform.position = position.position;
                    projectile.ApplyPower(direction * _attackPower);

                    await Task.Delay(TimeSpan.FromSeconds(_fireDelay), token);
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
                    AbstractProjectile projectile = _projectileSource.Get();
                    projectile.transform.position = position.position;
                    projectile.ApplyPower(direction * _attackPower);

                    await Task.Delay(TimeSpan.FromSeconds(_fireDelay / 2), token);
                }
            }
        }

        public void StopShooting()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}