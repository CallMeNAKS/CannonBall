using DG.Tweening;
using UnityEngine;

namespace CodeBase.Domain.Enemy.State
{
    public class StartState : IState
    {
        private readonly AbstractEnemy _enemy;

        public StartState(AbstractEnemy enemy)
        {
            _enemy = enemy;
        }
        
        public void Enter()
        {
            _enemy.transform.DOScale(_enemy.transform.localScale * 3, 15f);
            Debug.Log("Enter Start State");
            Execute();
        }

        public void Execute()
        {
            Debug.Log("Lift from the bottom");
        }

        public void Exit()
        {
            // _enemy.transform.DOComplete();
            Debug.Log("Exit Start State");
        }
    }
}