using TMPro;
using UnityEngine;

namespace CodeBase.Domain.Enemy.UI
{
    public class UIScoreTextChanger : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        
        [Header("Color")]
        [SerializeField] private Color _minColor = Color.white;
        [SerializeField] private Color _maxColor = Color.red;
        [SerializeField] private int _minScore = 0;
        [SerializeField] private int _maxScore = 10;
        
        public void ChangeScore(int newScore)
        {
            _scoreText.text = newScore.ToString();
            float t = Mathf.InverseLerp(_minScore, _maxScore, newScore);
            _scoreText.color = Color.Lerp(_minColor, _maxColor, t);
        }
    }
}