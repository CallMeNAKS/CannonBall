using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] protected float _attackPower = 1f;
        [SerializeField] protected Transform _target;
        [SerializeField] protected AbstractProjectilesSource _projectilesSource;

        public void SetTarget(Transform target)
        {
            _target = target;
        }
        public void SetProjectileSource(AbstractProjectilesSource projectilesSource)
        {
            _projectilesSource = projectilesSource;
        }
        public abstract void Attack();
        public abstract void Move();
        public abstract void Reset();
    }
}