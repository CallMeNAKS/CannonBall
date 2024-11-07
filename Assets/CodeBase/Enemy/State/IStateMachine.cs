namespace CodeBase.Domain.Enemy.State
{
    public interface IStateMachine
    {
        public void Enter(EnemyState stateType);
        public void RegisterState(EnemyState stateType, IState state);
    }
}