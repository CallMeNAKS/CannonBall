﻿using CodeBase.Domain.Enemy;
using CodeBase.Domain.PlayerInput;
using CodeBase.Domain.Text;
using Domain.Player;
using Domain.Shop;

namespace GameState
{
    public class GameState
    {
        private Shop _shop;
        private Player _player;
        private AbstractEnemy _enemy;
        private IPlayerInput _playerInput;
        private IOnStartState _onStartComponent;

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
            _shop.EndShoping += OnEndShoping;
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