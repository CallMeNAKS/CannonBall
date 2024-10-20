using CodeBase.Domain.CannonBall.Source;
using CodeBase.Domain.Enemy;
using Domain.Target;
using Domain.Target.Source;
using Domain.UI;
using UnityEngine;

namespace CodeBase.Domain
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private ObjectPoolBallSource _ballSource;
        [SerializeField] private PlayerInput.PlayerInput _playerInput;
        [SerializeField] private ObjectPoolTargetSource _targetSource;
        [SerializeField] private AbstractEnemy _abstractEnemy;
        [SerializeField] private GameObject _startGameUI;

        private void OnEnable()
        {
            _playerInput.RestartGame.AddListener(RestartGame);
            _playerInput.StartGameEvent.AddListener(StartGame);
        }

        public void RestartGame()
        {
            _scoreView.Reset();
            _ballSource.Reset();
            _targetSource.Reset();
            _abstractEnemy.Reset();
            _startGameUI.gameObject.SetActive(true);
        }

        private void StartGame()
        {
            _abstractEnemy.Move();
            _abstractEnemy.Attack();
            _startGameUI.gameObject.SetActive(false);
        }
        
    }
}