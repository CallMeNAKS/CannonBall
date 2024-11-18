using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Domain.Target.Source
{
    public class ObjectPoolProjectilesSource : AbstractProjectilesSource
    {
        [SerializeField] private AbstractProjectile projectilePrefab;
        [SerializeField] private float _targetLifeTime = 5f;
        
        private Queue<AbstractProjectile> _targetsQueue = new Queue<AbstractProjectile>();
        private List<AbstractProjectile> _allTargets = new List<AbstractProjectile>();
        
        public delegate void OnTargetHit();
        public event OnTargetHit OnTargetHitEvent;
        
        public override AbstractProjectile GetTarget()
        {
            if (_targetsQueue.TryDequeue(out var target))
            {
                target.gameObject.SetActive(true);
            }
            else
            {
                target = Instantiate(projectilePrefab);
                _allTargets.Add(target);
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
                _targetsQueue.Enqueue(projectile);
            }
        }
        
        public void OnHit()
        {
            OnTargetHitEvent?.Invoke();
        }
        
        public void Reset()
        {
            foreach (var target in _allTargets)
            {
                target.gameObject.SetActive(false);
                Destroy(target.gameObject);
            }

            _allTargets.Clear();
            _targetsQueue.Clear();
        }
    }
}