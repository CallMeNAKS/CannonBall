using CodeBase.Domain.Enemy;
using CodeBase.Domain.Enemy.Factory;
using CodeBase.Domain.Text;
using DIContainer.Factory;
using Domain.Player;
using Domain.Rocket;
using Domain.Target.Source;
using Domain.UI;
using UnityEngine;

namespace DIContainer

{
    public class SceneContext : MonoBehaviour
    {
        private DIContainer _container;

        [Header("Player")] [SerializeField] private Transform _playerSpawnPosition;
        [SerializeField] private Player _playerPrefab;

        [Header("Enemy")] [SerializeField] private Transform _enemySpawnPosition;
        [SerializeField] private AbstractEnemy _abstractEnemy;

        [Header("ProjectileSource")] [SerializeField]
        private AbstractProjectilesSource _projectilesSource;


        [Header("UI")] [SerializeField] private TextAnimation _startText;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private HealthView _healthView;

        private void Awake()
        {
            _container = new DIContainer();

            _container.RegisterSingleton<IFactory>(c => new GenericFactory());

            AbstractProjectilesSource projectileSourceService =
                _container.Resolve<IFactory>().Create(_projectilesSource);
            var projectileEventService = projectileSourceService.GetComponent<IProjectileEventService>();

            FactoryRegistration(projectileSourceService);

            // Создаём игрока и врага через фабрики
            var playerInstance = _container.Resolve<IPlayerFactory>().Create();
            var enemyInstance = _container.Resolve<IEnemyFactory>().Create(playerInstance.transform);

            // Регистрируем экземпляры
            _container.RegisterInstance<Player>("Player", playerInstance);
            _container.RegisterInstance<AbstractEnemy>("Enemy", enemyInstance);

            // Инициализация UI
            UIInitialization(projectileEventService, playerInstance);
        }

        private void UIInitialization(IProjectileEventService projectileEventService, Player playerInstance)
        {
            var scoreView = _container.Resolve<IFactory>().Create(_scoreView);
            scoreView.SetProjectilesListener(projectileEventService);

            var healthView = _container.Resolve<IFactory>().Create(_healthView);
            healthView.SetPlayerService(playerInstance);
        }

        private void FactoryRegistration(AbstractProjectilesSource source)
        {
            _container.RegisterSingleton<IPlayerFactory>
                (c => new PlayerFactory(_playerPrefab, _playerSpawnPosition));
            _container.RegisterSingleton<IEnemyFactory>
                (c => new EnemyFactory(_abstractEnemy, _enemySpawnPosition, source));
        }
    }
}