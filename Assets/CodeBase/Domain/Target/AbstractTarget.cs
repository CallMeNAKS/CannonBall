using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Domain.Target
{
    public abstract class AbstractTarget : MonoBehaviour
    {
        [SerializeField] protected int _score;

        public delegate void AddScoreOnHit(int score);
        public event AddScoreOnHit Hit;
        public Action Disabled;

        protected abstract void ChangeColor();
        protected abstract void ChangeScale();
        public abstract void ApplyPower(Vector3 force);

        protected void OnHit(int score)
        {
            Hit?.Invoke(score);
        }
    }
}