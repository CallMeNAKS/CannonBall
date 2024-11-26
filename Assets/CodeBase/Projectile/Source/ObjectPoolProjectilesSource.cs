using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Target;
using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Projectile.Source
{
    public class ObjectPoolProjectilesSource : AbstractProjectilesSource
    {
        [SerializeField] private AbstractProjectile projectilePrefab;
        [SerializeField] private float _targetLifeTime = 5f;
        
        private readonly Queue<AbstractProjectile> _projectiles = new Queue<AbstractProjectile>();
        
        public event Action OnTargetHit;
        
        public override AbstractProjectile GetTarget()
        {
            if (_projectiles.TryDequeue(out var target))
            {
                target.gameObject.SetActive(true);
            }
            else
            {
                target = Instantiate(projectilePrefab);
            }
            
            StartCoroutine(TurnOffTarget(target));
            return target;
        }

        private IEnumerator TurnOffTarget(AbstractProjectile projectile)
        {
            yield return new WaitForSeconds(_targetLifeTime);
            if (projectile != null && projectile.gameObject != null)
            {
                projectile.gameObject.SetActive(false);
                _projectiles.Enqueue(projectile);
            }
        }
        
        public void OnHit()
        {
            OnTargetHit?.Invoke();
        }
        
        public void Reset()
        {
            foreach (var target in _projectiles)
            {
                target.gameObject.SetActive(false);
                Destroy(target.gameObject);
            }

            _projectiles.Clear();
        }
    }
}