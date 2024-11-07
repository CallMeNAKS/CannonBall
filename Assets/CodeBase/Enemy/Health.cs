using System;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;

        public float MaxHealth { get; private set; }

        public event Action<float> DamageTaken;
        public event Action HealthReducedHalf;
        public event Action HealthEnded;

        private void Awake()
        {
            MaxHealth = _health;
        }


        public void TakeDamage(float damage)
        {
            _health -= damage;
            
            if (_health >= 0)
            {
                DamageTaken?.Invoke(_health);

                if (_health <= MaxHealth / 2)
                {
                    HealthReducedHalf?.Invoke();
                }
            }
            
            if (_health <= 0)
            {
                HealthEnded?.Invoke();
            }
        }
    }
}