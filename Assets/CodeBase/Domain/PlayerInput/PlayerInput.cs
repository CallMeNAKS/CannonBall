using System;
using CodeBase.Domain.AxisBases;
using CodeBase.Domain.Cannon;
using UnityEngine;

namespace CodeBase.Domain.PlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private AbstractCannon _cannon;
        [SerializeField] private AbstractAxisBases _axisBases;

        public event Action StartGameClicked;
        public event Action RestartGameClicked;
        public event Action RocketLaunced;
        
        private bool _isGameStarted;

        private void Update()
        {
            if (Input.anyKeyDown && !_isGameStarted)
            {
                StartGame();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _cannon.Shoot();
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                _cannon.RocketShoot();
            }

            CannonRotationControl();
        }

        private void CannonRotationControl()
        {
            var input = new Vector2(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));
            _axisBases.Rotate(input * Time.deltaTime);
        }

        private void StartGame()
        {
            _isGameStarted = true;
            StartGameClicked?.Invoke();
        }

        private void RestartGame()
        {
            _isGameStarted = false;
            RestartGameClicked?.Invoke();
        }
    }
}