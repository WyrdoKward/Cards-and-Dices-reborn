using Assets._Scripts.Cards.Common;
using UnityEngine;

namespace Assets._Scripts.StateMachines
{
    public abstract class CardBaseState : IState
    {
        #region IState
        public abstract void Enter(IStateContext uncastController);
        public abstract void UpdateState(IStateContext uncastController);
        public abstract void Exit(IStateContext uncastController);
        #endregion

        protected CardController cardController;
        protected GameObject cardGO;


        protected void CastContext(IStateContext uncastContext)
        {
            cardController = (CardController)uncastContext;
            cardGO = cardController.gameObject;
        }
    }
}
