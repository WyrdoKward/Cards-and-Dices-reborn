using UnityEngine;

namespace Assets._Scripts.StateMachines.Game
{
    internal class GameStartedState : GameBaseState
    {
        public override void Enter(IStateContext gameManager)
        {
            Debug.Log("Loading GameStartedState...");
        }

        public override void Exit(IStateContext gameManager)
        {
            Debug.Log("Done loading GameStartedState");
        }

        public override void HandleInput(IStateContext gameManager)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState(IStateContext gameManager)
        {
            throw new System.NotImplementedException();
        }
    }
}
