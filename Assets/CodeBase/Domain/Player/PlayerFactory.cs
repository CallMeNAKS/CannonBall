using CodeBase.Domain.PlayerInput;
using UnityEngine;

namespace Domain.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly Player _player;
        private readonly Transform _transform;

        public PlayerFactory(Player player, Transform transform)
        {
            _player = player;
            _transform = transform;
        }

        public Player Create()
        {
            return Player.Instantiate(_player, _transform);
        }
    }
}
