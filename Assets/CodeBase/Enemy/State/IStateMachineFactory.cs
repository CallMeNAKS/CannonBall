namespace CodeBase.Domain.Enemy.State
{
    public interface IStateMachineFactory
    {
        public EnemyStateMachine Create(AbstractEnemy enemy);
    }
}