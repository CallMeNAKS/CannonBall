using System;

namespace DIContainer.Card
{
    
    public class Card : AbstractCard
    {
        public override event Action CardSelected;

        protected override void HandleCardClick()
        {
            CardSelected?.Invoke();
        }
    }
}