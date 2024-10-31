using UnityEngine;

namespace DIContainer.Factory
{
    public class GenericFactory : IFactory
    {
        public T Create<T>(T objectToCreate) where T : Object
        {
            T newObject = Object.Instantiate(objectToCreate);
            return newObject;
        }
    }
}