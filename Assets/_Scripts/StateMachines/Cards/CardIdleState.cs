using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards
{
    public class CardIdleState : CardBaseState
    {
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);
            Debug.Log("CardIdleState ENTER");
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
            Debug.Log("CardIdleState EXIT");
        }

        public override void OnMouseDrag(IStateContext uncastController)
        {
            CastContext(uncastController);
            //TODO Ajouter contrôle de mvt minimum
            cardController.SwitchState(cardController.MovingState);
        }

        public override void OnMouseUp(IStateContext cardController)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
        }
    }
}
