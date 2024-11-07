using System.Threading.Tasks;

namespace CodeBase.Domain.Enemy
{
    public interface IShooter
    {
        public  Task StartShooting();
        public Task StartShootingSecond();
        public void StopShooting();
    }
}