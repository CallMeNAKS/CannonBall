using System;
using System.Collections;
using CodeBase.Domain.CannonBall;
using CodeBase.Domain.CannonBall.Source;
using CodeBase.Domain.PlayerInput;
using Domain.Player;
using Domain.Rocket;
using UnityEngine;

namespace CodeBase.Domain.Cannon
{
    public class Cannon : AbstractCannon
    {
        [SerializeField] private float _power;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AbstractBallSource _ballSource;
        [SerializeField] private Player _player;
        
        private IPlayerInput _playerInput;

        [SerializeField] private float _ballReloadTime = 0.1f;

        [Header("Rocket")]
        [SerializeField] private Rocket _rocket;
        [SerializeField] private float _rocketReloadTime = 5f;

        private bool _canShootBall = true;
        private bool _canShootRocket = true;
        
        public event Action RocketShooted;

        private void Awake()
        {
            StartCoroutine(ReloadRocket());
        }

        public override void Shoot()
        {
            if (!_canShootBall) return;
            
            Ball ball = _ballSource.New();
            ball.transform.position = _shootPoint.position;
            ball.ApplyPower(_shootPoint.forward * _power);
            
            _canShootBall = false;
            StartCoroutine(ReloadBall());
        }

        public override void RocketShoot()
        {
            if (!_canShootRocket) return;
            
            RocketShooted?.Invoke();
            var rocket = Instantiate(_rocket);
            rocket.transform.position = _shootPoint.position;
            rocket.ApplyPower(_shootPoint.forward * _power);
            
            _canShootRocket = false;
            StartCoroutine(ReloadRocket());
        }
        
        private IEnumerator ReloadBall()
        {
            yield return new WaitForSeconds(_ballReloadTime);
            _canShootBall = true;
        }

        private IEnumerator ReloadRocket()
        {
            yield return new WaitForSeconds(_rocketReloadTime);
            _canShootRocket = true;
        }
    }
}
