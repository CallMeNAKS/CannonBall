using UnityEngine;

namespace DIContainer.Factory
{
    public interface IFactory
    {
        T Create<T>(T objectToCreate) where T : Object;
    }
}

