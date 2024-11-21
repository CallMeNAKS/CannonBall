using System;
using CodeBase.Domain.Enemy.State;
using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(IShooter))]
    [RequireComponent(typeof(IMover))]
    [DisallowMultipleComponent]
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public event Action OnDeath;

        protected Health HealthComponent { get; private set; }
        protected IStateMachine StateMachine { get; private set; }
        public IShooter Shooter { get; private set; }
        public IMover Mover { get; private set; }


        private void Awake()
        {
            HealthComponent = GetComponent<Health>();
            Shooter = GetComponent<IShooter>();
            Mover = GetComponent<IMover>();
        }

        public void Init(Transform target, AbstractProjectilesSource projectilesSource, IStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            Shooter.Init(target, projectilesSource);
        }

        public abstract void StartIdleState();
        public abstract void StartFighting();
        public abstract void Reset();

        protected void Death()
        {
            OnDeath?.Invoke();
        }
    }
}