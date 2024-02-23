using Assets._Scripts.Cards.Common;
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

            //if next => switch them to follow
            cardController.NextCardInStack?.Follow();
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
            TargetPosition = InputHelper.GetCursorPositionInWorld() - cardController.MouseDelta;
            //Debug.Log($"Dragging {cardGO}");
        }

        public override void OnMouseUp(IStateContext uncastController)
        {
            CastContext(uncastController);
            var targetCard = DragAndDropHelper.FindOverlappedCardIfExists(cardGO);
            cardController.SetPreviousCard(targetCard);
            Debug.Log($"targetCard = {targetCard}");
            if (targetCard == null)
            {
                cardController.SwitchState(cardController.IdleState);
                return;
            }

            //Snap UI on target card
            cardController.GetComponent<CardDisplay>().SnapOnCard(targetCard);

            // Si il y a une recette, on la lance
            var firstcardOfStack = StackHelper.GetFirstCardOfStack(cardGO);
            firstcardOfStack.RunOrResetIfRecipe();

            cardController.SwitchState(cardController.FollowingState);
        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
            _rectTransform.position = Vector2.Lerp(_rectTransform.position, TargetPosition, Time.deltaTime * GlobalVariables.LerpSpeed);

            if (MovingByMouse) return;

            // Si il est en "pilote auto" (Diperse par ex.), passe en idle si la cible est atteinte
            if ((Vector2)_rectTransform.position == TargetPosition)
            {
                cardController.SwitchState(cardController.IdleState);
            }
        }
    }
}
