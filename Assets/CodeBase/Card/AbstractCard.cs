using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DIContainer.Card
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class AbstractCard : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected string CardName;
        
        public abstract event Action CardSelected;
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(CardName);
            HandleCardClick();
        }

        protected abstract void HandleCardClick();
    }
}