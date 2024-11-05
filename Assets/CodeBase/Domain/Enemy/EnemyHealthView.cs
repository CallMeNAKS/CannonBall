using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Domain.Enemy
{
    public class EnemyHealthView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private AbstractEnemy _abstractEnemy;

        private void OnEnable()
        {
            _abstractEnemy.DamageTaken += ChangeHealth;
         
            _slider.maxValue = _abstractEnemy.MaxHealth;
            _slider.value = _abstractEnemy.Health;
        }

        private void ChangeHealth(float value)
        {
            _slider.value = value;
        }

        private void OnDisable()
        {
            _abstractEnemy.DamageTaken -= ChangeHealth;
        }
    }
}