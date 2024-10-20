using System;
using DG.Tweening;
using UnityEngine;

namespace DIContainer.TestsCube
{
    public class CubeService : ICubeService
    {
        private readonly Transform _cubeTransform;

        public CubeService(Transform cubeTransform)
        {
            _cubeTransform = cubeTransform;
        }

        public void ExpandCube()
        {
            _cubeTransform.DOScale(new Vector3(2, 2, 2), 1f)
                .OnComplete(() => 
                {
                    CubeExpanded?.Invoke();
                });
        }

        public event Action CubeExpanded;
    }
}