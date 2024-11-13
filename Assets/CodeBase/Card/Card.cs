using UnityEngine;

namespace CodeBase.Card
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "NewCard", menuName = "Card/Card")]
    public class Card : ScriptableObject
    {
        [SerializeField] private string _cardName;
        [SerializeField] private string _description;
        [SerializeField] private CardType _bonusType;
        [SerializeField] private int _basePrice;
        // [SerializeField] private CardRarity _rarity;

        public string CardName => _cardName;
        public string Description => _description;
        public CardType BonusType => _bonusType;
        public int BasePrice => _basePrice;
        // public CardRarity Rarity => _rarity;
    }


    public enum CardType
    {
        FireRate,
        Overheat,
        Damage,
        ScoreBonus
    }
}