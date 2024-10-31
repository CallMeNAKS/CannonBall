using System;

namespace Domain.Player
{
    public interface IPlayerService
    {
        public event Action<int> PlayerTookDamage;
    }
}