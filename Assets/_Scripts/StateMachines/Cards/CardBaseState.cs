using Assets._Scripts.Cards.Common;
using UnityEngine;

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
        protected GameObject cardGO;

        public abstract void OnMouseDrag(IStateContext cardController);
        public abstract void OnMouseUp(IStateContext cardController);

        protected void CastContext(IStateContext uncastContext)
        {
            cardController = (CardController)uncastContext;
            cardGO = cardController.gameObject;
        }
    }
}
