using System;
using CodeBase.Money;
using CodeBase.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Domain.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private GameObject[] shopItems;
        [SerializeField] private CardSelectionPanel _cardSelectionPanel;
        public MoneyBank MoneyBank { get; private set; }
        
        [SerializeField] private Button _closeShopButton;
        
        public event Action EndShoping;

        private void OnEnable()
        {
            _closeShopButton.onClick.AddListener(CloseShop);
        }

        public void OpenShop()
        {
            _cardSelectionPanel.DrawCards();
        }

        public void CloseShop()
        {
            EndShoping?.Invoke();
        }

        private void OnDisable()
        {
            _closeShopButton.onClick.RemoveAllListeners();
        }
    }
}