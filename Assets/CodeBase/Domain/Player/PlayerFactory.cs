using CodeBase.Domain.PlayerInput;
using UnityEngine;

namespace Domain.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private DIContainer.DIContainer _container;
        private Player _player;

        public PlayerFactory(DIContainer.DIContainer container, Player player)
        {
            _container = container;
            _player = player;
        }

        public Player Create()
        {
            var player =  Player.Instantiate(_player);
            player.PlayerInput = _container.Resolve<IPlayerInput>();
            
            return player; 
        }
    }
}
