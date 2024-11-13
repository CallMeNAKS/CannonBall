using UnityEngine;

namespace CodeBase.Domain.Enemy.State
{
    public class LooseStage : IState
    {
        public void Enter()
        {
            Debug.Log("О нет как я мог проиграть, взрываюсь и падаю");
        }

        public void Execute()
        {
            Debug.Log("Падаю");
        }

        public void Exit()
        {
            Debug.Log("Exit LooseStage");
        }
    }
}