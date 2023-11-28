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
            Debug.Log("CardMovingState ENTER");
            _rectTransform = cardController.GetComponent<RectTransform>();
            cardController.IsBeingDragged = true;
            Cursor.visible = false;
            cardController.transform.localScale = GlobalVariables.CardBiggerScale;
            cardController.GetComponent<Canvas>().sortingOrder = GlobalVariables.OnDragCardSortingLayer;
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
            Debug.Log("CardMovingState EXIT");
            cardController.IsBeingDragged = false;
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

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);

        }
    }
}
