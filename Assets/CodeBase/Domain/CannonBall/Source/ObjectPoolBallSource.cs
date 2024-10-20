using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Domain.CannonBall.Source
{
    public class ObjectPoolBallSource : AbstractBallSource
    {
        [SerializeField] private Ball _ballTemplate;
        [SerializeField] private float _ballLifeTime = 5f;

        private Queue<Ball> _balls = new();
        private List<Ball> _ballPool = new();
        
        public override Ball New()
        {
            if (_balls.TryDequeue(out var newBall))
            {
                newBall.gameObject.SetActive(true);
            }
            else
            {
                newBall = Instantiate(_ballTemplate);
                _ballPool.Add(newBall);
            }

            StartCoroutine(CountDown(newBall));
            return newBall;
        }

        private IEnumerator CountDown(Ball ball)
        {
            yield return new WaitForSeconds(_ballLifeTime);
            if (ball != null && ball.gameObject != null) // Проверяем, был ли объект уничтожен
            {
            ball.gameObject.SetActive(false);
            _balls.Enqueue(ball);
            }
        }

        public void Reset()
        {
            foreach (var ball in _ballPool)
            {
                ball.gameObject.SetActive(false);
                Destroy(ball.gameObject);
            }

            _ballPool.Clear();
            _balls.Clear();
        }
    }
}