using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Domain.Enemy.UI
{
    public class ScoreTransfer : MonoBehaviour
    {
        [SerializeField] private Transform _placeToMove;
        [SerializeField] private Transform _textTransform;
        [SerializeField] private float _animationDuration = 1f;
        [SerializeField] private Vector3 _animationScale = Vector3.one * .5f;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _originalPosition = _textTransform.position;
        }

        private void OnEnable()
        {
            _textTransform.position = _originalPosition;
            Transfer();
        }

        public void Transfer()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_textTransform.DOScale(_animationScale, 0.3f))
                .Append(_textTransform.DOMove(_placeToMove.position, _animationDuration))
                .OnComplete(Deactivate);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}