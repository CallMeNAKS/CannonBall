using CodeBase.Domain.Enemy;
using CodeBase.Domain.PlayerInput;
using CodeBase.Domain.Text;
using Domain.Player;
using Domain.Shop;

namespace GameState
{
    public class GameState
    {
        private readonly Shop _shop;
        private readonly Player _player;
        private readonly AbstractEnemy _enemy;
        private readonly IPlayerInput _playerInput;
        private readonly IOnStartState _onStartComponent;

        public GameState(Shop shop, Player player, AbstractEnemy enemy, IPlayerInput playerInput,
            IOnStartState onStartComponent)
        {
            _shop = shop;
            _player = player;
            _enemy = enemy;
            _playerInput = playerInput;
            _onStartComponent = onStartComponent;
        }

        public void Initialize()
        {
            _shop.EndShoping += OnEndShopping;
            _player.PlayerLost += OnPlayerLost;
            _enemy.OnDeath += OnEnemyDeath;
            _playerInput.StartGameClicked += StartPlayMode;
        }

        public void StartGame()
        {
            _onStartComponent.OnStartGame();
        }

        private void StartPlayMode()
        {
            _onStartComponent.Exit();
            _enemy.CreateStateMachine();
        }

        private void OnEnemyDeath()
        {
            _shop.OpenShop();
        }

        private void OnPlayerLost()
        {
        }

        private void OnEndShopping()
        {
            _shop.gameObject.SetActive(false);
        }

        public void EndGame()
        {
            _shop.EndShoping -= OnEndShopping;
            _player.PlayerLost -= OnPlayerLost;
            _enemy.OnDeath -= OnEnemyDeath;
            _playerInput.StartGameClicked -= StartGame;
        }
    }
}