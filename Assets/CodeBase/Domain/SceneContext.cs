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
        [SerializeField] private Domain.Shop.Shop _shopUI;

        private void Awake()
        {
            _container = new DIContainer();
            RegisterServices();
            InitializeGame();
        }

        private void RegisterServices()
        {
            _container.RegisterSingleton<IFactory>(c => new GenericFactory());

            AbstractProjectilesSource projectileSourceService =
                _container.Resolve<IFactory>().Create(_projectilesSource);
            var projectileEventService = projectileSourceService.GetComponent<IProjectileEventService>();

            FactoryRegistration(projectileSourceService);

            var playerInstance = _container.Resolve<IPlayerFactory>().Create();
            var enemyInstance = _container.Resolve<IEnemyFactory>().Create(playerInstance.transform);

            _container.RegisterInstance<Player>("Player", playerInstance);
            _container.RegisterInstance<AbstractEnemy>("Enemy", enemyInstance);

            UIInitialization(projectileEventService, playerInstance);

            // Создание GameState через DI и регистрация
            _container.RegisterSingleton<GameState>(c =>
                new GameState(_container.Resolve<Domain.Shop.Shop>(), playerInstance, enemyInstance));
        }

        private void InitializeGame()
        {
            var gameState = _container.Resolve<GameState>();
            gameState.Initialize(); // Инициализируем GameState после создания
        }

        private void UIInitialization(IProjectileEventService projectileEventService, Player playerInstance)
        {
            var scoreView = _container.Resolve<IFactory>().Create(_scoreView);
            scoreView.SetProjectilesListener(projectileEventService);

            var healthView = _container.Resolve<IFactory>().Create(_healthView);
            healthView.SetPlayerService(playerInstance);

            var shopUI = _container.Resolve<IFactory>().Create(_shopUI);
            shopUI.OfflineShop();
            
            _container.RegisterInstance(shopUI);
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
