using UnityEngine;

namespace Assets._Scripts.StateMachines.Game
{
    internal class GameExitState : GameBaseState
    {
        private bool isDoneSaving = false;
        public override void Enter(IStateContext gameManager)
        {
            Debug.Log("Saving game...");
        }

        public override void Exit(IStateContext gameManager)
        {
            Debug.Log("Game saved, exit.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public override void HandleInput(IStateContext gameManager)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState(IStateContext gameManager)
        {
            //Save stuff...
            isDoneSaving = true;

            if (isDoneSaving)
                Exit(gameManager);
        }
    }
}
