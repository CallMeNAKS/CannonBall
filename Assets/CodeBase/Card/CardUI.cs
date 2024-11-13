using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Card
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardNameText;
        [SerializeField] private TMP_Text _descriptionText;
        // [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Button _selectButton;

        private Card _cardData;

        public void Initialize(Card cardData, System.Action<Card> onCardSelected)
        {
            _cardData = cardData;
        
            _cardNameText.text = cardData.CardName;
            _descriptionText.text = cardData.Description;

            // Допустим, что у нас есть спрайт, ассоциированный с редкостью (или цвет).
            // Для примера:
            // _rarityImage.color = GetRarityColor(cardData.Rarity.RarityName);

            // Назначаем действие на кнопку
            _selectButton.onClick.RemoveAllListeners();
            _selectButton.onClick.AddListener(() => onCardSelected?.Invoke(_cardData));
        }

        private Color GetRarityColor(string rarityName)
        {
            switch (rarityName)
            {
                case "Common": return Color.white;
                case "Rare": return Color.blue;
                case "Epic": return Color.magenta;
                default: return Color.gray;
            }
        }
    }
}