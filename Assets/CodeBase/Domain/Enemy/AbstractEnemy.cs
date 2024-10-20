using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] protected float _attackPower = 1f;
        
        public abstract void Attack();
        public abstract void Move();
        public abstract void Reset();
    }
}