using CodeBase.Domain.CannonBall;
using CodeBase.Domain.CannonBall.Source;
using UnityEngine;

namespace CodeBase.Domain.Cannon
{
    public class Cannon : AbstractCannon
    {
        [SerializeField] private float _power;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AbstractBallSource _ballSource;
        
        public override void Shoot()
        {
            Ball ball = _ballSource.New();
            ball.transform.position = _shootPoint.position;
            ball.ApplyPower(_shootPoint.forward * _power);
        }
    }
}