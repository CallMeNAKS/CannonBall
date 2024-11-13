using System.Collections.Generic;
using Domain.Player;
using TMPro;
using UnityEngine;

namespace Domain.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private HealthTextAnimation _healthTextAnimation;

        private IPlayerService _playerService;

        public void SetPlayerService(IPlayerService playerService)
        {
            _playerService = playerService;
            _playerService.PlayerTookDamage += ChangeHealthText;
        }

        private void ChangeHealthText(int health)
        {
            string text = "Health: " + health;
            _healthText.text = text;

            _healthTextAnimation.TextAnimation(_healthText);
        }

        private void OnDisable()
        {
            _playerService.PlayerTookDamage -= ChangeHealthText;
        }
    }
}