using CodeBase.Domain.PlayerInput;
using DIContainer.Factory;
using UnityEngine;

namespace Domain.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly Player _player;

        public PlayerFactory(Player player)
        {
            _player = player;
        }

        public Player Create()
        {
            return Player.Instantiate(_player);
        }

        public T Create<T>(T objectToCreate) where T : Object
        {
            throw new System.NotImplementedException();
        }
    }
}
