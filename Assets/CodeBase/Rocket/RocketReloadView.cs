using System;
using System.Collections;
using CodeBase.Domain.Cannon;
using CodeBase.Domain.PlayerInput;
using TMPro;
using UnityEngine;

namespace Domain.Rocket
{
    public class RocketReloadView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rocketReloadView;
        
        [SerializeField] private Cannon _playerInput;
        
        private void OnEnable()
        {
            ShowReloadTime();
            _playerInput.RocketShooted += ShowReloadTime;
        }

        private void ShowReloadTime()
        {
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            float reloadTime = 5.0f;
            _rocketReloadView.text = reloadTime.ToString("F1");
    
            while (reloadTime > 0)
            {
                yield return new WaitForSeconds(0.1f);
                reloadTime -= 0.1f;
                _rocketReloadView.text = reloadTime.ToString("F1");
            }
    
            _rocketReloadView.text = "0.0";
        }

        private void OnDisable()
        {
            _playerInput.RocketShooted -= ShowReloadTime;
        }
    }
}