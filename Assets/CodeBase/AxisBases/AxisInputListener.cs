using CodeBase.Domain.PlayerInput;
using UnityEngine;

namespace CodeBase.Domain.AxisBases
{
    public class AxisInputListener : AbstractAxisBases
    {
        [SerializeField] private AbstractAxisBases _origin;
        
        private IPlayerInput _playerInput;
        
        public void SunscribePlayerInput(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.CannonRotated += Rotate;
        }
        
        public override void Rotate(Vector2 vector)
        {
            _origin.Rotate(vector);
        }

        private void OnDisable()
        {
            _playerInput.CannonRotated -= Rotate;
        }
    }
}