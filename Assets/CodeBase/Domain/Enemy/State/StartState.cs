using UnityEngine;

namespace CodeBase.Domain.Enemy.State
{
    public class StartState : IState
    {
        public void Enter()
        {
            Debug.Log("Enter Start State");
            Execute();
        }

        public void Execute()
        {
            Debug.Log("Lift from the bottom");
        }

        public void Exit()
        {
            Debug.Log("Exit Start State");
        }
    }
}