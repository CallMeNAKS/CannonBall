using System;
using DIContainer.Card;
using UnityEngine;
using UnityEngine.UIElements;

namespace DIContainer.Shop
{
    public class PickUpgradeCard : MonoBehaviour
    {
        [SerializeField] private AbstractCard[] _cards;

        private void OnEnable()
        {
            foreach (var card in _cards)
            {
                card.CardSelected += CardChoose;
            }
        }

        private void CardChoose()
        {
            Debug.Log("Pick up card");
        }
        
        private void OnDisable()
        {
            foreach (var card in _cards)
            {
                card.CardSelected -= CardChoose;
            }
        }
    }
}