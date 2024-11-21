using CodeBase.PostEffect;
using Level;
using UnityEngine;
using UnityEngine.Rendering;

public class EntryPoint : MonoBehaviour
{
    private DIContainer.DIContainer _container;
    private StartLevel _startLevel;

    [Header("Player")] [SerializeField] private Transform _playerSpawnPosition;
    [Header("Enemy")] [SerializeField] private Transform _enemySpawnPosition;
    
    [SerializeField] private VolumeEffects _volume;

    private void Awake()
    {
        _container = new DIContainer.DIContainer();
        
        LevelRegister();
        InitializeGame();
    }

    private void LevelRegister()
    {
        _startLevel = new StartLevel(_container, _playerSpawnPosition, _enemySpawnPosition, _volume);
        _startLevel.InitializeLevel();
    }

    private void InitializeGame()
    {
        var gameState = _container.Resolve<GameState.GameState>();
        gameState.Initialize();
        gameState.StartGame();
    }
}