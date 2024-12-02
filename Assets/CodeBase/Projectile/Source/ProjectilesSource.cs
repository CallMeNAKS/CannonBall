using UnityEngine;

namespace Domain.Target.Source
{
    public abstract class ProjectilesSource : MonoBehaviour
    {
        public abstract AbstractProjectile Get();
    }
}