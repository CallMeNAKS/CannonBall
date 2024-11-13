using System;
using UnityEngine;

namespace CodeBase.Domain.PlayerInput
{
    public class MouthPlayerInput : MonoBehaviour, IPlayerInput
    {
        public event Action StartGameClicked;
        public event Action RestartGameClicked;
        public event Action RocketLaunced;
        public event Action Shooted;
        public event Action<Vector2> CannonRotated;

        private bool _isGameStarted;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
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

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Shooted?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                RocketLaunced?.Invoke();
            }

            CannonRotationControl();
        }

        private void CannonRotationControl()
        {
            var input = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            CannonRotated?.Invoke(input * Time.deltaTime);
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