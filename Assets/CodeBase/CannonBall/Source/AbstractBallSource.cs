using UnityEngine;

namespace CodeBase.Domain.CannonBall.Source
{
    public abstract class AbstractBallSource : MonoBehaviour
    {
        public abstract Ball New();
    }
}