using System;
using Domain.Target;
using Domain.Target.Source;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Domain.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private IProjectileEventService _projectilesListener;
        [SerializeField] private TMP_Text _scoreText;

        private int _score;
        
        private void UpdateUI(int score)
        {
            _scoreText.text = score.ToString();
        }

        private void AddPointsOnHit(int score)
        {
            _score = (_score + score);
            UpdateUI(_score);
        }

        public void Reset()
        {
            _score = 0;
            UpdateUI(_score);
        }

        public void SetProjectilesListener(IProjectileEventService projectilesListener)
        {
            _projectilesListener = projectilesListener;
            _projectilesListener.OnHit += AddPointsOnHit;
        }

        private void OnDisable()
        {
            _projectilesListener.OnHit -= AddPointsOnHit;
        }
    }
}