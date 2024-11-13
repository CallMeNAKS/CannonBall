using System;
using CodeBase.Money;
using CodeBase.Shop;
using UnityEngine;

namespace Domain.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private GameObject[] shopItems;
        [SerializeField] private CardSelectionPanel _cardSelectionPanel;
        public MoneyBank MoneyBank { get; private set; }
        
        public event Action EndShoping;

        public void OpenShop()
        {
            ToggleShopState(true);
            _cardSelectionPanel.DrawCards();
        }

        public void CloseShop()
        {
            ToggleShopState(false);
        }

        private void ToggleShopState(bool state)
        {
            foreach (var shopItem in shopItems)
            {
                shopItem.SetActive(state);
            }
        }
    }
}