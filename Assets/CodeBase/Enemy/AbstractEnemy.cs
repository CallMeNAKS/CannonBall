using System;
using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(IShooter))]
    [RequireComponent(typeof(IMover))]
    [DisallowMultipleComponent]
    public abstract class AbstractEnemy : MonoBehaviour //TODO разгрузить класс
    {
        public event Action OnDeath;

        protected IShooter Shooter { get; private set; }
        protected Health HealthComponent { get; private set; }
        protected IMover Mover { get; private set; }

        private void Awake()
        {
            HealthComponent = GetComponent<Health>();
            Shooter = GetComponent<IShooter>();
            Mover = GetComponent<IMover>();
        }

        public void Init(Transform target, AbstractProjectilesSource projectilesSource)
        {
            Shooter.Init(target, projectilesSource);
        }

        public abstract void Reset();
        public abstract void CreateStateMachine();

        protected void Death()
        {
            OnDeath?.Invoke();
        }
    }
}