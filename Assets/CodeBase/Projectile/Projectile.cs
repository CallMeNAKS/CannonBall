using System;
using System.Collections;
using CodeBase.Domain.CannonBall;
using DG.Tweening;
using UnityEngine;

namespace Domain.Target
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : AbstractProjectile
    {
        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;
        private Material _defaultMaterial;

        private int _changedScore;
        
        [Header("Material")]
        [SerializeField] private Material _newMaterial;

        [Header("Scale")]
        [SerializeField] private float _scaleSpeed = 1f;
        [SerializeField] private Vector3 _targetScale = new Vector3(2, 2, 2);

        public override event Action<int> Hit;
        public override event Action<AbstractProjectile> Disabled;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            _defaultMaterial = _meshRenderer.material;
        }

        private void OnEnable()
        {
            transform.DOScale(_targetScale, _scaleSpeed).SetEase(Ease.InOutSine);
            _changedScore = _score;
        }

        public override void ApplyPower(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball)
            {
                Hit?.Invoke(_changedScore);
                ChangeColor();
                ChangeScale();
                _changedScore++;
            }
        }

        protected override void ChangeColor()
        {
            _meshRenderer.material = _newMaterial;
            StartCoroutine(DelayChangeColor(0.1f, _defaultMaterial));
        }

        protected override void ChangeScale()
        {
            transform.DOPunchScale(Vector3.one, 0.1f, 3);
            transform.DOComplete();
        }

        private IEnumerator DelayChangeColor(float delayTime, Material newColor)
        {
            yield return new WaitForSeconds(delayTime);
            _meshRenderer.material = newColor;
        }

        private void OnDisable()
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
            StopAllCoroutines();
            _meshRenderer.material = _defaultMaterial;
            _rigidbody.velocity = Vector3.zero;
            Disabled?.Invoke(this);
            _changedScore = 0;
        }
    }
}