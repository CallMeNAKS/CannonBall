using UnityEngine;

namespace Domain.Target.Source
{
    public abstract class AbstractProjectilesSource : MonoBehaviour
    {
        public abstract AbstractProjectile GetTarget();
    }
}