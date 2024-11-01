using System;
using Domain.Target.Source;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] protected float _attackPower = 1f;
        [SerializeField] protected Transform _target;
        [SerializeField] protected AbstractProjectilesSource _projectilesSource;
        
        [SerializeField] protected float _health = 100f;
        
        public float Health { get => _health; private set => _health = value; }
        public abstract event Action<float> DamageTaken; 
        public event Action OnDeath; 

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
        public abstract void TakeDamage(float damage);

        protected void Death()
        {
            OnDeath?.Invoke();
        }
    }
}