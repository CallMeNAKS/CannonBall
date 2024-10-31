using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly AbstractEnemy _enemyPrefab;
        private readonly Transform _spawnPoint;
        private readonly AbstractProjectilesSource _projectilesSource;

        public EnemyFactory(AbstractEnemy enemyPrefab, Transform spawnPoint, AbstractProjectilesSource projectilesSource)
        {
            _enemyPrefab = enemyPrefab;
            _spawnPoint = spawnPoint;
            _projectilesSource = projectilesSource;
        }
        
        public AbstractEnemy Create(Transform player)
        {
            AbstractEnemy enemyInstance = Object.Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);

            enemyInstance.SetTarget(player);
            enemyInstance.SetProjectileSource(_projectilesSource);
            enemyInstance.Move();
            enemyInstance.Attack();
            
            return enemyInstance;
        }
    }
}