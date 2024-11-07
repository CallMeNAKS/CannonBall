using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public class EnemyMover : MonoBehaviour, IMover
    {
        [SerializeField] private float _timeToChangePlace = 2f;
        
        private Coroutine _movingCoroutine;
        
        public void Move()
        {
            _movingCoroutine = StartCoroutine(StartMoving());
        }

        public void AlternativeMove()
        {
            throw new System.NotImplementedException();
        }

        public void StopMoving()
        {
            if (_movingCoroutine != null)
            {
                StopCoroutine(_movingCoroutine);
                _movingCoroutine = null;
            }
            transform.DOKill();
        }

        private IEnumerator StartMoving()
        {
            MoveToRandomPosition();
            yield return new WaitForSeconds(_timeToChangePlace);
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