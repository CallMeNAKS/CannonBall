using Domain.Player;
using TMPro;
using UnityEngine;

namespace Domain.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;

        private IPlayerService _playerService;

        public HealthView(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        private void ChangeHealthText(int health)
        {
            string text = health.ToString();
            _healthText.text = text;
            Debug.Log($"Health changed to {text}");
        }
        
        public void SetPlayerService(IPlayerService playerService)
        {
            _playerService = playerService;
            _playerService.PlayerTookDamage += ChangeHealthText;
        }

        private void OnDisable()
        {
            _playerService.PlayerTookDamage -= ChangeHealthText;
        }
    }
}