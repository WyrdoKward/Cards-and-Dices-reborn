namespace Assets._Scripts.StateMachines
{
    internal abstract class CardBaseState : IState
    {
        #region IState
        public abstract void Enter(IStateContext card);
        public abstract void UpdateState(IStateContext card);
        public abstract void Exit(IStateContext card);
        #endregion


        public abstract void HandleInput(IStateContext card);
    }
}
