using Domain.Target;
using Domain.Target.Source;
using TMPro;
using UnityEngine;

namespace Domain.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TargetListener _targetListener;
        [SerializeField] private TMP_Text _scoreText;

        private int _score;

        private void OnEnable()
        {
            _targetListener.OnTargetHitEvent += AddPointsOnHit;
        }

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
    }
}