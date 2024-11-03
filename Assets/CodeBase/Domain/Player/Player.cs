using System;
using Domain.Target;
using UnityEngine;

namespace Domain.Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class Player : MonoBehaviour, IPlayerService
    {
        public event Action<int> PlayerTookDamage;
        
        public event Action PlayerLost;

        [SerializeField] private int _health = 6;

        private void OnEnable()
        {
            InvokeEvent();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Projectile>())
            {
                PlayerTakesDamage();
            }
        }

        private void PlayerTakesDamage()
        {
            if (_health > 0)
            {
                _health--;
                InvokeEvent();
            }
            else
            {
                PlayerLost?.Invoke();
            }
        }

        private void InvokeEvent()
        {
            PlayerTookDamage?.Invoke(_health);
        }
    }
}