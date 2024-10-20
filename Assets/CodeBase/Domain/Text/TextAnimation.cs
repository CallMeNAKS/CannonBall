using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

namespace CodeBase.Domain.Text
{
    public class TextAnimation : MonoBehaviour
    {
        [SerializeField] private float _transformMultiplier = 1.2f;
        [SerializeField] private float _animationDuration = 0.5f;

        private void OnEnable()
        {
            StartCoroutine(TextAnimationCoroutine());
        }

        private IEnumerator TextAnimationCoroutine()
        {
            while (true)
            {
                transform.DOScale(_transformMultiplier, _animationDuration).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(_animationDuration);

                transform.DOScale(1.0f, _animationDuration).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(_animationDuration);
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}