using System;
using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour //TODO разгрузить класс
    {
        [SerializeField] private Transform[] _projectilesPositions;
        private Transform _target;
        private AbstractProjectilesSource _projectilesSource;

        [SerializeField] private float _health = 100f;
        private float _maxHealth = 100f;
        
        [SerializeField] private float _attackPower = 1f;
        [SerializeField] private float _fireRate = 2f;

        private Shooter _shooter;
        
        public abstract event Action<float> DamageTaken;
        public event Action OnDeath;

        protected Transform[] ProjectilesPositions { get{ return _projectilesPositions; } }
        protected float FireRate { get{return _fireRate;} }
        protected Shooter Shooter
        {
            get { return _shooter; }
            set { _shooter = value; }
        }

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

        public float MaxHealth
        {
            get => _maxHealth;
        }

        public void Awake()
        {
            _maxHealth = _health;
        }

        public void Init(Transform target, AbstractProjectilesSource projectilesSource)
        {
            _target = target;
            _projectilesSource = projectilesSource;
            CreateShooter();
        }

        public abstract void CreateShooter();
        public abstract void Move();
        public abstract void Reset();
        public abstract void TakeDamage(float damage);
        public abstract void CreateStateMachine();

        protected void Death()
        {
            OnDeath?.Invoke();
        }
    }
    
    
}