using CodeBase.Domain.Enemy.State;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField]
        public EnemyState[] States { get; private set; }
    }
}