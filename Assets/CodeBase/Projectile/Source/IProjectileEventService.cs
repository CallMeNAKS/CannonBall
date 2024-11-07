using System;

namespace Domain.Target.Source
{
    public interface IProjectileEventService
    {
        public event Action<int> OnHit; 
    }
}