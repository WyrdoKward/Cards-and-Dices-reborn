using Assets._Scripts.Cards.Common;
using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards.MovementState
{
    public class CardMovingState : CardBaseMovementState
    {
        public bool MovingByMouse = false;
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);

            _rectTransform = cardController.GetComponent<RectTransform>();
            cardController.LastPosition = _rectTransform.position;

            Cursor.visible = false;
            cardController.transform.localScale = GlobalVariables.CardBiggerScale;
            cardController.GetComponent<Canvas>().sortingOrder = GlobalVariables.OnDragCardSortingLayer;

            //if next => switch thenm to follow
            var nextCard = cardController.NextCardInStack;
            if (nextCard != null)
            {
                var nextController = nextCard.GetComponent<CardController>();
                nextController.SwitchState(nextController.FollowingState);
            }
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
            MovingByMouse = false;
            Cursor.visible = true;
            cardController.transform.localScale = GlobalVariables.CardDefaultScale;
            cardController.GetComponent<Canvas>().sortingOrder = GlobalVariables.DefaultCardSortingLayer;
        }

        public override void OnMouseDrag(IStateContext uncastController)
        {
            CastContext(uncastController);
            TargetPosition = InputHelper.GetCursorPositionInWorld(_rectTransform);
        }

        public override void OnMouseUp(IStateContext uncastController)
        {
            CastContext(uncastController);
            var targetCard = DragAndDropHelper.FindOverlappedCardIfExists(cardGO);
            cardController.SetPreviousCard(targetCard);

            if (targetCard == null)
            {
                cardController.SwitchState(cardController.IdleState);
                return;
            }

            //Snap on target card
            cardController.GetComponent<CardDisplay>().SnapOnCard(targetCard);
            var firstcardOfStack = StackHelper.GetFirstCardOfStack(cardGO);

            if (firstcardOfStack != null && firstcardOfStack.GetComponent<CardLogic>().HasReceipe())
            {
                firstcardOfStack.GetComponent<CardController>().SwitchState(cardController.RunningState);
                cardController.SwitchState(cardController.IdleState); //Voir si création d'un lockedState ?
                return;
            }

            cardController.SwitchState(cardController.FollowingState);

        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
            _rectTransform.position = Vector2.Lerp(_rectTransform.position, TargetPosition, Time.deltaTime * GlobalVariables.LerpSpeed);

            if (MovingByMouse && (Vector2)_rectTransform.position == TargetPosition)
            {
                cardController.SwitchState(cardController.IdleState);
            }
        }
    }
}
