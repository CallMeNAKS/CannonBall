using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.TestsCube
{
    public class CubeScaler : MonoBehaviour, ICube
    {
        private Vector3 _scale = new Vector3(2f, 2f, 2f);
        private float _duration = 1f;

        public event Action Scaled;

        private void Start()
        {
            transform.DOScale(_scale, _duration).OnComplete(OnEndScale);
        }

        private void OnEndScale()
        {
            Scaled?.Invoke();
        }
    }
}