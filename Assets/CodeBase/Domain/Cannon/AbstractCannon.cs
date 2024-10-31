using System;
using UnityEngine;

namespace CodeBase.Domain.Cannon
{
    public abstract class AbstractCannon: MonoBehaviour
    {
        public abstract void Shoot();
        public abstract void RocketShoot();

        public abstract event Action RocketShooted;
    }
}