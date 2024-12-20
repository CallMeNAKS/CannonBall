﻿using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Domain.Text
{
    public class OnStartTextAnimation : MonoBehaviour, IOnStartState
    {
        [SerializeField] private float _transformMultiplier = 1.2f;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Transform _textTransform;
        
        private Coroutine _textCoroutine;

        public void OnStartGame()
        {
            _textCoroutine = StartCoroutine(TextAnimationCoroutine());
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

        public void Exit()
        {
            StopCoroutine(_textCoroutine);
            _textTransform.gameObject.SetActive(false);
        }
    }
}