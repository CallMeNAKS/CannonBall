namespace CodeBase.Domain.Enemy
{
    public interface IMover
    {
        public void Move();
        public void AlternativeMove();
        public void StopMoving();
    }
}