using System.Threading.Tasks;
using Domain.Target.Source;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    public interface IShooter
    {
        public void Init(Transform target, AbstractProjectilesSource projectileSource); // Нормально ли так делать? 
        public Task StartShooting();
        public Task StartShootingSecond();
        public void StopShooting();
    }
}