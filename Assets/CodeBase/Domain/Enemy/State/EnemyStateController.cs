using System.Collections.Generic;
using Unity.VisualScripting;

namespace CodeBase.Domain.Enemy.State
{
    public enum EnemyState
    {
        Start,
        Fight,
        SecondFight,
        Loose
    }
    public class EnemyStateController : IStateMachine
    {
        private IState _currentState;
        private Dictionary<EnemyState, IState> _registeredStates = new();
        
        public void Enter(EnemyState stateType)
        {
            IState newState = ChangeState(stateType);
            newState.Enter();
        }

        public void RegisterState(EnemyState stateType, IState state)
        {
            _registeredStates[stateType] = state;
        }

        public IState ChangeState(EnemyState newState)
        {
            if (_currentState != null)
                _currentState.Exit();
            
            IState state = _registeredStates[newState];
            _currentState = state;

            return state;
        }
    }

    public interface IStateMachine
    {
        public void Enter(EnemyState stateType);
        public void RegisterState(EnemyState stateType, IState state);
    }
}