using CodeBase.Domain.Enemy;
using UnityEngine;

namespace Domain.Rocket
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        [SerializeField] private float _speed;
        [SerializeField] private float _damage;

        public float Damage
        {
            get { return _damage; }
            private set { _damage = value; }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void ApplyPower(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        private void OnCollisionEnter(Collision other)
        {
            var enemy = other.gameObject.GetComponentInParent<AbstractEnemy>();

            if (enemy)
            {
                enemy.TakeDamage(_damage);
            }
        }
    }
}