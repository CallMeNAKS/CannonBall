using CodeBase.Configs;
using CodeBase.Domain.Enemy;
using CodeBase.Domain.Enemy.Factory;
using CodeBase.Domain.Enemy.State;
using CodeBase.Domain.PlayerInput;
using CodeBase.Domain.Text;
using CodeBase.PostEffect;
using DIContainer.Factory;
using Domain.Player;
using Domain.Target.Source;
using Domain.UI;
using UnityEngine;
using UnityEngine.Rendering;

namespace Level
{
    public class StartLevel
    {
        private readonly DIContainer.DIContainer _container;

        private readonly Transform _playerSpawnPosition; //TODO аддресебалс
        private readonly Player _playerPrefab = Resources.Load<Player>("Player/Cannon Player");
        private readonly MouthPlayerInput _input = Resources.Load<MouthPlayerInput>("Player/MouthInput");

        private readonly Transform _enemySpawnPosition;
        private readonly AbstractEnemy _abstractEnemy = Resources.Load<AbstractEnemy>("Enemy/RobotEnemy");

        private readonly AbstractProjectilesSource
            _projectilesSource = Resources.Load<AbstractProjectilesSource>("Enemy/Source");

        private readonly OnStartTextAnimation _onStartText = Resources.Load<OnStartTextAnimation>("UI/StartText");
        private readonly ScoreView _scoreView = Resources.Load<ScoreView>("UI/ScoreView");
        private readonly HealthView _healthView = Resources.Load<HealthView>("UI/HealthView");
        private readonly Domain.Shop.Shop _shopUI = Resources.Load<Domain.Shop.Shop>("UI/Shop");
        private readonly EnemyConfig _enemyConfig = Resources.Load<EnemyConfig>("Configs/Enemy/Robot");
        private readonly VolumeEffects _volume;

        public StartLevel(DIContainer.DIContainer container, Transform playerSpawnPosition, Transform enemySpawnPosition, VolumeEffects volume)
        {
            _container = container;
            _playerSpawnPosition = playerSpawnPosition;
            _enemySpawnPosition = enemySpawnPosition;
            _volume = volume;
        }

        public void InitializeLevel()
        {
            RegisterFactories();
            RegisterInput();
            InitializePlayer();
            InitializeEnemy();
            InitializeUI();
            RegisterGameState();
        }

        private void InitializeEnemy()
        {
            var enemyInstance = _container.Resolve<IEnemyFactory>().Create(_container.Resolve<Player>("Player").transform);
            enemyInstance.transform.position = _enemySpawnPosition.position;
            _container.RegisterInstance("Enemy", enemyInstance);
        }

        private void InitializePlayer()
        {
            var playerInstance = _container.Resolve<IPlayerFactory>().Create();
            playerInstance.transform.position = _playerSpawnPosition.position;
            _container.RegisterInstance("Player", playerInstance);
        }

        private void RegisterInput()
        {
            var playerInput = CreateByFactory(_input);
            _container.RegisterSingleton<IPlayerInput>(c => playerInput);
        }

        private void RegisterFactories()
        {
            FactoryRegistration();
            ProjectilesSourceRegistration();

            _container.RegisterSingleton<IPlayerFactory>(
                c => new PlayerFactory(_container, _playerPrefab));
            _container.RegisterSingleton<IEnemyFactory>(
                c => new EnemyFactory(_abstractEnemy, _container.Resolve<AbstractProjectilesSource>(), _enemyConfig, _container.Resolve<IStateMachineFactory>()));
        }

        private void FactoryRegistration()
        {
            _container.RegisterSingleton<IFactory>(c => new GenericFactory());
            _container.RegisterSingleton<IStateMachineFactory>(c => new StateMachineFactory(_volume));
        }

        private void ProjectilesSourceRegistration()
        {
            AbstractProjectilesSource projectileSourceService =
                _container.Resolve<IFactory>().Create(_projectilesSource);

            var projectileEventService = projectileSourceService.GetComponent<IProjectileEventService>();
            _container.RegisterSingleton(c => projectileEventService);

            var projectileFactory = projectileEventService as AbstractProjectilesSource;
            _container.RegisterSingleton<AbstractProjectilesSource>(c => projectileFactory);
        }

        private void InitializeUI()
        {
            IOnStartState onStartUI = CreateByFactory(_onStartText);
            _container.RegisterInstance(onStartUI);

            var scoreView = CreateByFactory(_scoreView);
            scoreView.SetProjectilesListener(_container.Resolve<IProjectileEventService>());

            var healthView = CreateByFactory(_healthView);
            healthView.SetPlayerService(_container.Resolve<Player>("Player"));

            var shopUI = CreateByFactory(_shopUI);
            _container.RegisterInstance(shopUI);
        }

        private T CreateByFactory<T>(T prefab) where T : Component
        {
            return _container.Resolve<IFactory>().Create(prefab);
        }

        private void RegisterGameState()
        {
            _container.RegisterSingleton<GameState.GameState>(c =>
                new GameState.GameState(
                    _container.Resolve<Domain.Shop.Shop>(),
                    _container.Resolve<Player>("Player"),
                    _container.Resolve<AbstractEnemy>("Enemy"),
                    _container.Resolve<IPlayerInput>(),
                    _container.Resolve<IOnStartState>()));
        }
    }
}