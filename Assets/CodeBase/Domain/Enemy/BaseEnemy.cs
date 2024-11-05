using System;
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
        
        private Vector3 _startPosition;
        
        public override event Action<float> DamageTaken;

        public override void Attack()
        {
            StartCoroutine(StartShooting());
        }
        
        public override void Move()
        {
            _startPosition = transform.position;
            MoveToRandomPosition();
        }

        public override void Reset()
        {
            StopAllCoroutines();
            transform.DOKill();
            transform.position = _startPosition;
        }

        private IEnumerator StartShooting()
        {
            while (true)
            {
                for (int i = 0; i < _projectilesPositions.Count; i++)
                {
                    _projectilesPositions[i].LookAt(Target.transform.position);
                    Vector3 direction = Target.position + new Vector3(0, 1f, 0) - _projectilesPositions[i].position;
                    AbstractProjectile projectile = ProjectilesSource.GetTarget();
                    projectile.transform.position = _projectilesPositions[i].position;
                    projectile.ApplyPower(direction * AttackPower);
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
            if (Health > 0)
            {
                Health -= damage;
                DamageTaken?.Invoke(Health);
            }
            if (Health <= 0)
            {
                Death();
            }
        }
    }
}