using CodeBase.Domain.Enemy.UI;
using DG.Tweening;
using Domain.Target.Source;
using TMPro;
using UnityEngine;

namespace Domain.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private ScoreTextSource _scoreTextSource;
        private IProjectileEventService _projectilesListener;
        private int _score;

        public void SetProjectilesListener(IProjectileEventService projectilesListener)
        {
            _projectilesListener = projectilesListener;
            _projectilesListener.OnHit += AddPointsOnHit;
        }

        private void UpdateUI(int score)
        {
            _scoreText.text = "Score: " + score;
    
            _scoreText.transform.DOScale(1.3f, 0.2f) // увеличиваем до 130%
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                    _scoreText.transform.DOScale(1f, 0.2f) // возвращаемся к 100%
                        .SetEase(Ease.InBack)
                );
        }

        private void AddPointsOnHit(int score)
        {
            _score = (_score + score);
            ShowScoreText(score);
            UpdateUI(_score);
        }

        private void ShowScoreText(int score)
        {
            UIScoreTextChanger textScore = _scoreTextSource.Get();
            textScore.gameObject.SetActive(true);
            textScore.ChangeScore(score);
        }

        public void Reset()
        {
            _score = 0;
            UpdateUI(_score);
        }

        private void OnDisable()
        {
            _projectilesListener.OnHit -= AddPointsOnHit;
        }
    }
}