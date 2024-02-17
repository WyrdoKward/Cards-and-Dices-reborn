using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards.MovementState
{
    public class CardIdleState : CardBaseMovementState
    {
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);
            cardGO.transform.localScale = GlobalVariables.CardDefaultScale;

            _rectTransform = cardController.GetComponent<RectTransform>();

            cardGO.GetComponent<Canvas>().sortingOrder = StackHelper.ComputeOrderInLayer(cardGO);

            //if next => switch them to idle as well
            cardController.NextCardInStack?.Idle();
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
            cardController.LastPosition = _rectTransform.position;
        }

        public override void OnMouseDrag(IStateContext uncastController)
        {
            CastContext(uncastController);
            cardController.SwitchState(cardController.MovingState);
        }

        public override void OnMouseUp(IStateContext uncastController)
        {
            CastContext(uncastController);
        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
        }
    }
}
