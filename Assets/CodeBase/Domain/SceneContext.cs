using UnityEngine;

namespace DIContainer
{
    public class SceneContext : MonoBehaviour
    {
        private DIContainer _container;

        [Header("Player")] [SerializeField] private Transform _playerSpawnPosition;

        [Header("Enemy")] [SerializeField] private Transform _enemySpawnPosition;

        private StartLevel _startLevel;

        private void Awake()
        {
            _container = new DIContainer();
            LevelRegister();
            InitializeGame();
        }

        private void LevelRegister()
        {
            _startLevel = new StartLevel(_container, _playerSpawnPosition, _enemySpawnPosition);
            _startLevel.InitializeLevel();
        }

        private void InitializeGame()
        {
            var gameState = _container.Resolve<GameState>();
            gameState.Initialize();
            gameState.StartGame();
        }
    }
}