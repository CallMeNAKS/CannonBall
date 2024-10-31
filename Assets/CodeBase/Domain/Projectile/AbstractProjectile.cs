using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Domain.Target
{
    public abstract class AbstractProjectile : MonoBehaviour
    {
        [SerializeField] protected int _score;

        public abstract event Action<int> Hit;
        public abstract event Action<AbstractProjectile> Disabled;

        protected abstract void ChangeColor();
        protected abstract void ChangeScale();
        public abstract void ApplyPower(Vector3 force);
    }
}