using CodeBase.Configs;
using CodeBase.Domain.Enemy.State;
using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly AbstractEnemy _enemyPrefab;
        private readonly AbstractProjectilesSource _projectilesSource;
        private readonly EnemyConfig _config;
        private readonly IStateMachineFactory _stateMachineFactory;

        public EnemyFactory(AbstractEnemy enemyPrefab, AbstractProjectilesSource projectilesSource, EnemyConfig config, IStateMachineFactory stateMachineFactory)
        {
            _enemyPrefab = enemyPrefab;
            _projectilesSource = projectilesSource;
            _config = config;
            _stateMachineFactory  = stateMachineFactory;
        }
        
        public AbstractEnemy Create(Transform player)
        {
            AbstractEnemy enemyInstance = Object.Instantiate(_enemyPrefab);

            var stateMachine = _stateMachineFactory.Create(enemyInstance);
            
            enemyInstance.Init(player, _projectilesSource, stateMachine);
            
            return enemyInstance;
        }
    }
}