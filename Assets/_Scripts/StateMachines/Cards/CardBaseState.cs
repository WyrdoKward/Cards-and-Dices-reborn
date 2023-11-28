using Assets._Scripts.Cards.Common;

namespace Assets._Scripts.StateMachines
{
    public abstract class CardBaseState : IState
    {
        #region IState
        public abstract void Enter(IStateContext card);
        public abstract void UpdateState(IStateContext card);
        public abstract void Exit(IStateContext card);
        #endregion


        protected CardController cardController;
        public void CastContext(IStateContext uncastContext)
        {
            cardController = (CardController)uncastContext;
        }

        public abstract void OnMouseDrag(IStateContext cardController);
    }
}
