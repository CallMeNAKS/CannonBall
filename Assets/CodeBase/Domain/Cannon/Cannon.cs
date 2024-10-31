using System;
using System.Collections;
using CodeBase.Domain.CannonBall;
using CodeBase.Domain.CannonBall.Source;
using Domain.Rocket;
using UnityEngine;

namespace CodeBase.Domain.Cannon
{
    public class Cannon : AbstractCannon
    {
        [SerializeField] private float _power;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AbstractBallSource _ballSource;
        
        [Header("Rocket")]
        [SerializeField] private Rocket _rocket;
        [SerializeField] private float _reloadTime = 5f;
        private bool _isReloading = false;
        
        public override event Action RocketShooted;

        private void Awake()
        {
            StartCoroutine(Timer());
        }

        public override void Shoot()
        {
            Ball ball = _ballSource.New();
            ball.transform.position = _shootPoint.position;
            ball.ApplyPower(_shootPoint.forward * _power);
        }

        public override void RocketShoot()
        {
            if (_isReloading == false)
                return;
            RocketShooted?.Invoke();
            StartCoroutine(Timer());
            var rocket = Instantiate(_rocket);
            rocket.transform.position = _shootPoint.position;
            rocket.ApplyPower(_shootPoint.forward * _power);
        }
        
        private IEnumerator Timer()
        {
            _isReloading = false;
            var reloadTime = _reloadTime;
    
            while (reloadTime > 0)
            {
                yield return new WaitForSeconds(0.1f);
                reloadTime -= 0.1f;
            }
    
            _isReloading = true;
        }
    }
}