using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Domain.UI
{
    public class HealthTextAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _healthTextPrefab;
        private readonly Queue<GameObject> _healthTextQueue = new();
        
        
        public void TextAnimation(TMP_Text healthText)
        {
            ChangeColor(healthText);
            ChangeScale(healthText);

            ActivateDynamicText();
        }

        private void ChangeColor(TMP_Text healthText)
        {
            healthText.DOColor(Color.red, 0.2f)
                .OnComplete(() =>
                    healthText.DOColor(Color.white, 0.5f)
                        .SetEase(Ease.OutQuad)
                );
        }

        private void ChangeScale(TMP_Text healthText)
        {
            healthText.transform.DOScale(1.3f, 0.2f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                    healthText.transform.DOScale(1f, 0.2f)
                        .SetEase(Ease.InBack)
                );
        }

        private void ActivateDynamicText()
        {
            if (_healthTextQueue.TryDequeue(out var text))
            {
                text.SetActive(true);
            }
            else
            {
                _healthTextQueue.Enqueue(Instantiate(_healthTextPrefab));
            }
        }
    }
}