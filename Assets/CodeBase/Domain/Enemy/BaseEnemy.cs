using System.Collections;
using Domain.Target;
using Domain.Target.Source;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.Serialization;

namespace CodeBase.Domain.Enemy
{
    public class BaseEnemy : AbstractEnemy
    {
        [SerializeField] private AbstractTargetSource _targetSource;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _timeToChangePlace = 2f;
        
        private Transform _defaultTransform;

        private void Awake()
        {
            _defaultTransform = transform;
        }

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
            transform.position = _defaultTransform.position;
        }

        private IEnumerator StartShooting()
        {
            while (true)
            {
                Vector3 direction = _playerTransform.position + new Vector3(0, 0.5f, 0) - transform.position;
                AbstractTarget projectile = _targetSource.GetTarget();
                projectile.transform.position = transform.position;
                projectile.ApplyPower(direction * _attackPower);
                yield return new WaitForSeconds(2f);
            }
        }

        private void MoveToRandomPosition()
        {
            float randomX = Random.Range(-10f, 10f); 
            float randomY = Random.Range(3f, 10f); 
            
            Vector3 targetPosition = new Vector3(randomX, randomY, transform.position.z);

            transform.DOMove(targetPosition, _timeToChangePlace).OnComplete(MoveToRandomPosition);
        }
    }
}