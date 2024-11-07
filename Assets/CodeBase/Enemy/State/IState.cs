namespace CodeBase.Domain.Enemy.State
{
    public interface IState
    {
        public void Enter();
        public void Execute();
        public void Exit();
    }
}