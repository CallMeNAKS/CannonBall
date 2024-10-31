using CodeBase.Domain.PlayerInput;

namespace Domain.Player
{
    public interface IPlayerFactory
    {
        public Player Create();
    }
}