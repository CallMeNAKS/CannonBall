using CodeBase.Domain.Enemy;
using CodeBase.Domain.Enemy.Factory;
using CodeBase.Domain.Text;
using DIContainer.Factory;
using Domain.Player;
using Domain.Target.Source;
using Domain.UI;
using UnityEngine;

namespace DIContainer
{
    public class StartLevel
    {
        private DIContainer _container;

        private Transform _playerSpawnPosition;
        private Player _playerPrefab = Resources.Load<Player>("Player/Cannon Player");

        private Transform _enemySpawnPosition;
        private AbstractEnemy _abstractEnemy = Resources.Load<AbstractEnemy>("Enemy/RobotEnemy");

        private AbstractProjectilesSource
            _projectilesSource = Resources.Load<AbstractProjectilesSource>("Enemy/Source");

        private TextAnimation _startText = Resources.Load<TextAnimation>("UI/StartText");
        private ScoreView _scoreView = Resources.Load<ScoreView>("UI/ScoreView");
        private HealthView _healthView = Resources.Load<HealthView>("UI/HealthView");
        private Domain.Shop.Shop _shopUI = Resources.Load<Domain.Shop.Shop>("UI/Shop");

        public StartLevel(DIContainer container, Transform playerSpawnPosition, Transform enemySpawnPosition)
        {
            _container = container;
            _playerSpawnPosition = playerSpawnPosition;
            _enemySpawnPosition = enemySpawnPosition;
        }

        public void InitializeLevel()
        {
            RegisterFactories();
            RegisterInstances();
            InitializeUI();
            RegisterGameState();
        }

        private void RegisterFactories()
        {
            FactoryRegistration();
            ProjectilesSourceRegistration();

            _container.RegisterSingleton<IPlayerFactory>(
                c => new PlayerFactory(_playerPrefab));
            _container.RegisterSingleton<IEnemyFactory>(
                c => new EnemyFactory(_abstractEnemy, _container.Resolve<AbstractProjectilesSource>()));
        }

        private void RegisterInstances()
        {
            var playerInstance = _container.Resolve<IPlayerFactory>().Create();
            playerInstance.transform.position = _playerSpawnPosition.position;

            var enemyInstance = _container.Resolve<IEnemyFactory>().Create(playerInstance.transform);
            enemyInstance.transform.position = _enemySpawnPosition.position;
            enemyInstance.Move();


            _container.RegisterInstance("Player", playerInstance);
            _container.RegisterInstance("Enemy", enemyInstance);
        }

        private void InitializeUI()
        {
            var scoreView = CreateAndRegisterUIComponent(_scoreView);
            scoreView.SetProjectilesListener(_container.Resolve<IProjectileEventService>());

            var healthView = CreateAndRegisterUIComponent(_healthView);
            healthView.SetPlayerService(_container.Resolve<Player>("Player"));

            var shopUI = CreateAndRegisterUIComponent(_shopUI);
            shopUI.OfflineShop();
            _container.RegisterInstance(shopUI);
        }

        private T CreateAndRegisterUIComponent<T>(T prefab) where T : Component
        {
            return _container.Resolve<IFactory>().Create(prefab);
        }

        private void RegisterGameState()
        {
            _container.RegisterSingleton<GameState>(c =>
                new GameState(
                    _container.Resolve<Domain.Shop.Shop>(),
                    _container.Resolve<Player>("Player"),
                    _container.Resolve<AbstractEnemy>("Enemy")));
        }

        private void ProjectilesSourceRegistration()
        {
            AbstractProjectilesSource projectileSourceService =
                _container.Resolve<IFactory>().Create(_projectilesSource);
            _container.RegisterSingleton<AbstractProjectilesSource>(c => projectileSourceService);

            var projectileEventService = projectileSourceService.GetComponent<IProjectileEventService>();
            _container.RegisterSingleton<IProjectileEventService>(c => projectileEventService);
        }

        private void FactoryRegistration()
        {
            _container.RegisterSingleton<IFactory>(c => new GenericFactory());
        }
    }
}