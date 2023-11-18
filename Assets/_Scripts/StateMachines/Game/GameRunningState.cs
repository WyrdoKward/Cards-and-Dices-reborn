using Managers;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Game
{
    internal class GameRunningState : GameBaseState
    {
        public override void Enter(IStateContext gameManager)
        {
            //Debug.Log("Entering GameRunningState");
        }

        public override void Exit(IStateContext gameManager)
        {
            //Debug.Log("Exit GameRunningState");
        }

        public override void HandleInput(IStateContext gameManager)
        {
            var castGameManager = (GameManager)gameManager;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                castGameManager.SwitchState(castGameManager.PausedState);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                castGameManager.SwitchState(castGameManager.ExitState);
            }
        }

        public override void UpdateState(IStateContext gameManager)
        {
        }
    }
}
