using Domain.Rocket;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private AbstractEnemy _abstractEnemy;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Rocket>())
            {
                var rocket = other.gameObject.GetComponent<Rocket>();
                Debug.Log(rocket.name);
                _abstractEnemy.TakeDamage(rocket.Damage);
                rocket.gameObject.SetActive(false);
                Debug.Log("Damage teked");
            }
        }
    }
}