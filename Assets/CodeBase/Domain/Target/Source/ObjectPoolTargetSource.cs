using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.Target.Source
{
    public class ObjectPoolTargetSource : AbstractTargetSource
    {
        [SerializeField] private AbstractTarget _targetPrefab;
        [SerializeField] private float _targetLifeTime = 5f;
        
        private Queue<AbstractTarget> _targetsQueue = new Queue<AbstractTarget>();
        private List<AbstractTarget> _allTargets = new List<AbstractTarget>();
        
        public delegate void OnTargetHit();
        public event OnTargetHit OnTargetHitEvent;

        
        public override AbstractTarget GetTarget()
        {
            if (_targetsQueue.TryDequeue(out var target))
            {
                target.gameObject.SetActive(true);
            }
            else
            {
                target = Instantiate(_targetPrefab);
                _allTargets.Add(target);
            }
            
            StartCoroutine(TurnOffTarget(target));
            return target;
        }

        private IEnumerator TurnOffTarget(AbstractTarget target)
        {
            yield return new WaitForSeconds(_targetLifeTime);
            if (target != null && target.gameObject != null) // Проверяем, был ли объект уничтожен
            {
                target.gameObject.SetActive(false);
                _targetsQueue.Enqueue(target);
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