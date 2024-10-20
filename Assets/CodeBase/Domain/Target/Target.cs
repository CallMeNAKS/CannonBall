﻿using System;
using System.Collections;
using CodeBase.Domain.CannonBall;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

namespace Domain.Target
{
    [RequireComponent(typeof(Rigidbody))]
    public class Target : AbstractTarget
    {
        private Rigidbody _rigidbody;
        private MeshRenderer _meshRenderer;
        private Material _defaultMaterial;
        [Header("Material")] [SerializeField] private Material _newMaterial;

        [Header("Scale")] [SerializeField] private float _scaleSpeed = 1f;
        [SerializeField] private Vector3 _targetScale = Vector3.one * 2f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            _defaultMaterial = _meshRenderer.material;
        }

        public override void ApplyPower(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.gameObject.GetComponent<Ball>())
            {
                OnHit(_score);
                ChangeColor();
                ChangeScale();
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

        private void OnEnable()
        {
            transform.DOScale(_targetScale, _scaleSpeed).SetEase(Ease.InOutSine);
        }

        private void OnDisable()
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
            StopAllCoroutines();
            _meshRenderer.material = _defaultMaterial;
            _rigidbody.velocity = Vector3.zero;
            Disabled?.Invoke();
        }
    }
}