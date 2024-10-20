using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace DIContainer.TestsCube
{
    public class SecondCubeService
    {
        private readonly ICubeService _firstCubeService;
        private readonly Transform _secondCubeTransform;

        public SecondCubeService(ICubeService firstCubeService, Transform secondCubeTransform)
        {
            _firstCubeService = firstCubeService;
            _secondCubeTransform = secondCubeTransform;

            _firstCubeService.CubeExpanded += OnFirstCubeExpand;
        }

        private void OnFirstCubeExpand()
        {
            _secondCubeTransform.DOScale(new Vector3(2, 2, 2), 1f);
        }
    }
}