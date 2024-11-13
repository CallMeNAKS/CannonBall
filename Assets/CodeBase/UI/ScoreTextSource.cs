using System.Collections.Generic;
using CodeBase.Domain.Enemy.UI;
using UnityEngine;

namespace Domain.UI
{
    public class ScoreTextSource : MonoBehaviour
    {
        [SerializeField] private UIScoreTextChanger _scoreTextPrefab;
        private Queue<UIScoreTextChanger> _scoreTextQueue = new Queue<UIScoreTextChanger>();

        public UIScoreTextChanger Get()
        {
            if (_scoreTextQueue.TryDequeue(out var scoreTextChanger))
            {
                return scoreTextChanger;
            }

            var scoreTextPrefab = Instantiate(_scoreTextPrefab);
            _scoreTextQueue.Enqueue(scoreTextPrefab);
            return scoreTextPrefab;
        }
    }
}