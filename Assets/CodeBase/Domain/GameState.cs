using CodeBase.Domain.Enemy;
using CodeBase.Domain.PlayerInput;
using CodeBase.Domain.Text;
using Domain.Player;

namespace DIContainer
{
    public class GameState
    {
        private Domain.Shop.Shop _shop;
        private Player _player;
        private AbstractEnemy _enemy;
        private IPlayerInput _playerInput;
        private IStartState _startComponent;

        public GameState(Domain.Shop.Shop shop, Player player, AbstractEnemy enemy, IPlayerInput playerInput, IStartState startComponent)
        {
            _shop = shop;
            _player = player;
            _enemy = enemy;
            _playerInput = playerInput;
            _startComponent = startComponent;
        }

        public void Initialize()
        {
            _shop.EndShoping += OnEndShoping;
            _player.PlayerLost += OnPlayerLost;
            _enemy.OnDeath += OnEnemyDeath;
            _playerInput.StartGameClicked += StartPlayMode;
        }

        public void StartGame()
        {
            _startComponent.OnStartGame();
        }

        private void StartPlayMode()
        {
            _startComponent.Exit();
            _enemy.CreateStateMachine();
            _enemy.Move();
        }

        private void OnEnemyDeath()
        {
              _shop.OpenShop();
        }

        private void OnPlayerLost()
        {
            throw new System.NotImplementedException();
        }

        private void OnEndShoping()
        {
            _shop.gameObject.SetActive(false);
        }

        public void EndGame()
        {
            _shop.EndShoping -= OnEndShoping;
            _player.PlayerLost -= OnPlayerLost;
            _enemy.OnDeath -= OnEnemyDeath;
            _playerInput.StartGameClicked -= StartGame;
        }
    }
}    