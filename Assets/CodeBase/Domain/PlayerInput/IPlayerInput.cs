using System;
using UnityEngine;

namespace CodeBase.Domain.PlayerInput
{
    public interface IPlayerInput
    {
        public event Action StartGameClicked;
        public event Action RestartGameClicked;
        public event Action RocketLaunced;
        public event Action Shooted;
        public event Action<Vector2> CannonRotated;
    }
}