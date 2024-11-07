using System;
using CodeBase.Domain.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Domain.Target.Source
{
    public class ProjectilesListener : AbstractProjectilesSource, IProjectileEventService
    {
        [SerializeField] private AbstractProjectilesSource _source;

        private AbstractProjectile _projectile;

        public event Action<int> OnHit;

        public override AbstractProjectile GetTarget()
        {
            AbstractProjectile projectile = _source.GetTarget();
            _projectile = projectile;
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