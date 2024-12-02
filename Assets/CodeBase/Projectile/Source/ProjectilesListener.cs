using System;
using CodeBase.Domain.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Domain.Target.Source
{
    public class ProjectilesListener : ProjectilesSource, IProjectileEventService
    {
        [SerializeField] private ProjectilesSource _source;

        public event Action<int> OnHit;

        public override AbstractProjectile Get()
        {
            AbstractProjectile projectile = _source.Get();
            projectile.Hit += InvokeEvent;
            projectile.Disabled += Unsubscribe;
            return projectile;
        }

        private void InvokeEvent(int score)
        {
            OnHit?.Invoke(score);
        }

        private void Unsubscribe(AbstractProjectile abstractProjectile)
        {
            abstractProjectile.Hit -= InvokeEvent;
            abstractProjectile.Disabled -= Unsubscribe;
        }
    }
}