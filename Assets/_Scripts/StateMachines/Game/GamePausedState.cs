using Managers;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Game
{
    internal class GamePausedState : GameBaseState
    {
        public override void Enter(IStateContext gameManager)
        {
            Debug.Log("Pausing game...");
        }

        public override void Exit(IStateContext gameManager)
        {
            Debug.Log("Resuming game");
        }

        public override void HandleInput(IStateContext gameManager)
        {
            var castGameManager = (GameManager)gameManager;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                castGameManager.SwitchState(castGameManager.RunningState);
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
