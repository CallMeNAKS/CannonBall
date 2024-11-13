using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Card
{
    [CreateAssetMenu(fileName = "NewDeck", menuName = "Card/Deck")]
    public class Deck : ScriptableObject
    {
        [SerializeField] private Card[] _cards;
        [SerializeField] private CardRarity[] _cardRarity;
        
        public Card[] Cards => _cards;
        public CardRarity[] CardRarity => _cardRarity;

    }
}