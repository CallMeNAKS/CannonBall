using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Domain.Text
{
    public class TextAnimation : MonoBehaviour
    {
        [SerializeField] private float _transformMultiplier = 1.2f;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Transform _textTransform;

        private void OnEnable()
        {
            StartCoroutine(TextAnimationCoroutine());
        }

        private IEnumerator TextAnimationCoroutine()
        {
            while (true)
            {
                _textTransform.DOScale(_transformMultiplier, _animationDuration).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(_animationDuration);

                _textTransform.DOScale(1.0f, _animationDuration).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(_animationDuration);
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}