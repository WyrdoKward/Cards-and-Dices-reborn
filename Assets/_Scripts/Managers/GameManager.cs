using Assets._Scripts.StateMachines;
using Assets._Scripts.StateMachines.Game;
using UnityEngine;
using Utilities.Singletons;

namespace Managers
{
    public class GameManager : StaticInstance<GameManager>, IStateContext
    {
        private GameBaseState currentState;
        internal GameStartedState StartedState = new();
        internal GameRunningState RunningState = new();
        internal GamePausedState PausedState = new();
        internal GameExitState ExitState = new();

        // Start is called before the first frame update
        void Start()
        {
            currentState = StartedState;
            currentState.Enter(this);
            currentState.Exit(this);
            currentState = RunningState;
            currentState.Enter(this);
        }

        // Update is called once per frame
        void Update()
        {
            currentState.UpdateState(this);
            if (Input.anyKeyDown)
            {
                currentState.HandleInput(this);
            }
        }

        #region IContextState
        public void SwitchState(IState newState)
        {
            currentState.Exit(this);
            currentState = (GameBaseState)newState;
            currentState.Enter(this);
        }
        #endregion
    }

}