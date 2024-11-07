using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Domain.CannonBall
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ApplyPower(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
        
        private void OnEnable()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        private void OnCollisionEnter(Collision other)
        {
            this.gameObject.SetActive(false);
        }
    }
}