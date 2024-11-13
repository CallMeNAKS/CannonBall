using CodeBase.Card;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Shop
{
    public class CardSelectionPanel : MonoBehaviour
    {
        // private Deck _deck = Resources.Load<Deck>("Cards/Deck Level_1");
        [SerializeField] private Deck _deck;

        private Card.Card[] _cards;
        private CardRarity[] _cardRarity;
        [SerializeField] private CardUI[] _cardUIs;
            
        private void Awake()
        {
            _cards = _deck.Cards;
            _cardRarity = _deck.CardRarity;
        }

        private void SelectCard(Card.Card card)
        {
            Debug.Log(card.CardName);
            ToggleCardUIs(false);
        }

        public void DrawCards()
        {
            foreach (var cardUI in _cardUIs)
            {
                ToggleCardUIs(true);
                
                var randomCard = _cards[Random.Range(0, _cards.Length)];
                cardUI.Initialize(randomCard, SelectCard);
            }
        }

        private void ToggleCardUIs(bool isSelected)
        {
            foreach (var cardUI in _cardUIs)
            {
                cardUI.gameObject.SetActive(isSelected);
            }
        }
    }
}