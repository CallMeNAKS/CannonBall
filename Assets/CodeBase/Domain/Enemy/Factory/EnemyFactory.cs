using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly AbstractEnemy _enemyPrefab;
        private readonly AbstractProjectilesSource _projectilesSource;

        public EnemyFactory(AbstractEnemy enemyPrefab, AbstractProjectilesSource projectilesSource)
        {
            _enemyPrefab = enemyPrefab;
            _projectilesSource = projectilesSource;
        }
        
        public AbstractEnemy Create(Transform player)
        {
            AbstractEnemy enemyInstance = Object.Instantiate(_enemyPrefab);

            enemyInstance.SetTarget(player);
            enemyInstance.SetProjectileSource(_projectilesSource);
            enemyInstance.CreateShooter();
            enemyInstance.CreateStateMachine();
            enemyInstance.Move();
            
            return enemyInstance;
        }
    }
}