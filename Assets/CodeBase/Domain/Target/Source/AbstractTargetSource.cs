using UnityEngine;

namespace Domain.Target.Source
{
    public abstract class AbstractTargetSource : MonoBehaviour
    {
        public abstract AbstractTarget GetTarget();
    }
}