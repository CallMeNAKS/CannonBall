using System;
using CodeBase.Domain.AxisBases;
using CodeBase.Domain.Cannon;
using CodeBase.Domain.PlayerInput;
using Domain.Target;
using UnityEngine;

namespace Domain.Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class Player : MonoBehaviour, IPlayerService
    {
        public event Action<int> PlayerTookDamage;
        
        private IPlayerInput _playerInput;
        private bool _isInputSeted;

        [SerializeField] private CannonInputListener _cannon;
        [SerializeField] private AxisInputListener _axisBases;
        

        public IPlayerInput PlayerInput // Как сделать лучше??? 
        {
            set
            {
                if (_isInputSeted)
                {
                    Debug.LogError("Input is sets");
                    return;
                }
                _playerInput = value;
                _cannon.SunscribePlayerInput(_playerInput);
                _axisBases.SunscribePlayerInput(_playerInput);
            }
        }
        
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