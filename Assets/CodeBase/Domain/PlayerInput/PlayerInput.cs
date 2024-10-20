using CodeBase.Domain.AxisBases;
using CodeBase.Domain.Cannon;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Domain.PlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private AbstractCannon _cannon;
        [SerializeField] private AbstractAxisBases _axisBases;

        public UnityEvent RestartGame = new();
        public UnityEvent StartGameEvent = new();
        
        private bool _isGameStarted;

        private void Update()
        {
            if (Input.anyKeyDown && !_isGameStarted)
            {
                _isGameStarted = true;
                StartGameEvent.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _isGameStarted = false;
                RestartGame.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _cannon.Shoot();
            }

            var input = new Vector2(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));
            _axisBases.Rotate(input * Time.deltaTime);
        }
    }
}