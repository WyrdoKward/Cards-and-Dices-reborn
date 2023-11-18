namespace Assets._Scripts.StateMachines
{
    internal abstract class GameBaseState : IState
    {
        #region IState
        public abstract void Enter(IStateContext gameManager);
        public abstract void UpdateState(IStateContext gameManager);
        public abstract void Exit(IStateContext gameManager);
        #endregion


        public abstract void HandleInput(IStateContext gameManager);
    }
}
