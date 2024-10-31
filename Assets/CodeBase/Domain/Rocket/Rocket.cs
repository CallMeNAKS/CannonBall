using System;
using UnityEngine;

namespace Domain.Rocket
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void ApplyPower(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
    }
}