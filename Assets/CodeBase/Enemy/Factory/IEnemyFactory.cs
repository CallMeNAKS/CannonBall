using CodeBase.Domain.Enemy.State;
using UnityEngine;

namespace CodeBase.Domain.Enemy.Factory
{
    public interface IEnemyFactory
    {
        AbstractEnemy Create(Transform player);
    }
}