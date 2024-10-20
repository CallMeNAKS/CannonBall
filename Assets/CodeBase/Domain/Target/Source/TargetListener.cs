using CodeBase.Domain.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Domain.Target.Source
{
    public class TargetListener : AbstractTargetSource
    {
        [SerializeField] private AbstractTargetSource _source;

        public delegate void OnTargetHit(int score);

        public event OnTargetHit OnTargetHitEvent;

        private AbstractTarget _target;

        public override AbstractTarget GetTarget()
        {
            AbstractTarget target = _source.GetTarget();
            _target = target;
            target.Hit += OnHit;
            target.Disabled += Unsubscribe;
            return target;
        }

        private void OnHit(int score)
        {
            OnTargetHitEvent?.Invoke(score);
        }

        private void Unsubscribe()
        {
            _target.Hit -= OnHit;
            _target.Disabled -= Unsubscribe;
        }
    }
}