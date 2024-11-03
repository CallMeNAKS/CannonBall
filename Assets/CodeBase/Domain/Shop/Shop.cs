using System;
using UnityEngine;

namespace Domain.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private GameObject[] shopItems;
        
        public event Action EndShoping;

        public void OfflineShop()
        {
            foreach (var item in shopItems)
            {
                item.SetActive(false);
                Debug.Log(item.activeSelf);
            }
        }

        public void OpenShop()
        {
            foreach (var shopItem in shopItems)
            {
                shopItem.SetActive(true);
                Debug.Log(shopItem.activeSelf);
            }
        }
    }
}