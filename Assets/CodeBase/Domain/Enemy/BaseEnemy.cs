﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Domain.Rocket;
using Domain.Target;
using Domain.Target.Source;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CodeBase.Domain.Enemy
{
    public class BaseEnemy : AbstractEnemy
    {
        [SerializeField] private float _timeToChangePlace = 2f;

        [SerializeField] private List<Transform> _projectilesPositions;
        
        private Vector3 _defaultTransform;
        
        private void Awake()
        {
            _defaultTransform = transform.position;
        }

        public override event Action<float> DamageTaken;

        public override void Attack()
        {
            StartCoroutine(StartShooting());
        }
        
        public override void Move()
        {
            MoveToRandomPosition();
        }

        public override void Reset()
        {
            StopAllCoroutines();
            transform.DOKill();
            transform.position = _defaultTransform;
        }

        private IEnumerator StartShooting()
        {
            while (true)
            {
                for (int i = 0; i < _projectilesPositions.Count; i++)
                {
                    _projectilesPositions[i].LookAt(_target.transform.position);
                    Vector3 direction = _target.position + new Vector3(0, 1f, 0) - _projectilesPositions[i].position;
                    AbstractProjectile projectile = _projectilesSource.GetTarget();
                    projectile.transform.position = _projectilesPositions[i].position;
                    projectile.ApplyPower(direction * _attackPower);
                    yield return new WaitForSeconds(2f);
                }
            }
        }

        private void MoveToRandomPosition()
        {
            float randomX = Random.Range(-10f, 10f); 
            float randomY = Random.Range(3f, 10f); 
            
            Vector3 targetPosition = new Vector3(randomX, randomY, transform.position.z);

            transform.DOMove(targetPosition, _timeToChangePlace).OnComplete(MoveToRandomPosition);
        }

        public override void TakeDamage(float damage)
        {
            if (_health > 0)
            {
                _health -= damage;
                DamageTaken?.Invoke(_health);
            }
            if (_health <= 0)
            {
                Death();
            }
        }
    }
}