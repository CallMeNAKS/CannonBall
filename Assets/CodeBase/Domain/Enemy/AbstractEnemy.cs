using System;
using Domain.Target.Source;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] private float _attackPower = 1f;
        [SerializeField] private Transform _target;
        [SerializeField] private AbstractProjectilesSource _projectilesSource;

        [SerializeField] private float _health = 100f;
        private float _maxHealth = 100f;

        public abstract event Action<float> DamageTaken;
        public event Action OnDeath;

        protected Transform Target
        {
            get => _target;
        }

        protected AbstractProjectilesSource ProjectilesSource
        {
            get => _projectilesSource;
        }

        protected float AttackPower
        {
            get => _attackPower;
        }

        public float Health
        {
            get => _health;
            protected set
            {
                if (value >= 0)
                {
                    _health = value;
                }
                else
                {
                    _health = 0;
                }
            }
        }
        
        public float MaxHealth { get => _maxHealth; }

        private void Awake()
        {
            _maxHealth = _health;
        }

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