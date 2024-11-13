using UnityEngine;

namespace CodeBase.Card
{
    [CreateAssetMenu(fileName = "NewRarity", menuName = "Card/CardRarity")]
    public class CardRarity : ScriptableObject
    {
        [SerializeField] private string _rarityName;
        [SerializeField] private float _bonusMultiplier;
        [SerializeField] private float _priceMultiplier;
        // [SerializeField] private AnimationClip _specialAnimation;

        public string RarityName => _rarityName;
        public float BonusMultiplier => _bonusMultiplier;
        public float PriceMultiplier => _priceMultiplier;
        // public AnimationClip SpecialAnimation => _specialAnimation;
    }
}