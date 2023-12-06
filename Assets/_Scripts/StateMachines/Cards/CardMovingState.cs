using Assets._Scripts.Cards.Common;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards
{
    public class CardMovingState : CardBaseState
    {
        private RectTransform _rectTransform;
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);

            _rectTransform = cardController.GetComponent<RectTransform>();
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

            Cursor.visible = true;
            cardController.transform.localScale = GlobalVariables.CardDefaultScale;
            cardController.GetComponent<Canvas>().sortingOrder = GlobalVariables.DefaultCardSortingLayer;
        }

        public override void OnMouseDrag(IStateContext uncastController)
        {
            CastContext(uncastController);
            var targetPosition = InputHelper.GetCursorPositionInWorld(_rectTransform);

            _rectTransform.position = Vector3.Lerp(_rectTransform.position, targetPosition, Time.deltaTime * GlobalVariables.LerpSpeed);
        }

        public override void OnMouseUp(IStateContext uncastController)
        {
            CastContext(uncastController);
            var targetCard = DragAndDropHelper.FindOverlappedCardIfExists(cardGO);
            cardController.SetPreviousCard(targetCard);

            //Snap on target card
            cardController.GetComponent<CardDisplay>().SnapOnCard(targetCard);

            if (targetCard != null)
                cardController.SwitchState(cardController.FollowingState);
            else
                cardController.SwitchState(cardController.IdleState);
        }

        public override void UpdateState(IStateContext uncastController)
        {
            //CastContext(uncastController);

        }
    }
}
