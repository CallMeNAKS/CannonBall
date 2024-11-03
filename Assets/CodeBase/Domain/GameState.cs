using CodeBase.Domain.Enemy;
using Domain.Player;
using UnityEngine;

namespace DIContainer
{
    public class GameState
    {
        private Domain.Shop.Shop _shop;
        private Player _player;
        private AbstractEnemy _enemy;

        public GameState(Domain.Shop.Shop shop, Player player, AbstractEnemy enemy)
        {
            _shop = shop;
            _player = player;
            _enemy = enemy;
        }

        public void Initialize()
        {
            _shop.EndShoping += OnEndShoping;
            _player.PlayerLost += OnPlayerLost;
            _enemy.OnDeath += OnEnemyDeath;
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
    }
}    