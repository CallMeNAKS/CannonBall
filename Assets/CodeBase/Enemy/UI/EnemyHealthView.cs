using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Domain.Enemy.UI
{
    public class EnemyHealthView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.DamageTaken += ChangeHealth;
         
            _slider.maxValue = _health.MaxHealth;
            _slider.value = _health.MaxHealth;
        }

        private void ChangeHealth(float value)
        {
            _slider.value = value;
        }

        private void OnDisable()
        {
            _health.DamageTaken -= ChangeHealth;
        }
    }
}