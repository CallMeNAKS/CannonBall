using CodeBase.PostEffect;

namespace CodeBase.Domain.Enemy.State
{
    public class StateMachineFactory : IStateMachineFactory
    {
        private readonly VolumeEffects _volumeEffects;

        public StateMachineFactory(VolumeEffects volumeEffects)
        {
            _volumeEffects = volumeEffects;
        }
        
        public EnemyStateMachine Create(AbstractEnemy enemy)
        {
            var stateMachine = new EnemyStateMachine();
            
            var startState = new StartState(enemy);
            var fightState = new FightState(enemy.Shooter, enemy.Mover, _volumeEffects);
            var secondFightState = new SecondFightState(enemy.Shooter, enemy.Mover);
            var looseStage = new LooseStage();

            stateMachine.RegisterState(EnemyState.Start, startState);
            stateMachine.RegisterState(EnemyState.Fight, fightState);
            stateMachine.RegisterState(EnemyState.SecondFight, secondFightState);
            stateMachine.RegisterState(EnemyState.Loose, looseStage);
            
            return stateMachine;
        }
    }
}