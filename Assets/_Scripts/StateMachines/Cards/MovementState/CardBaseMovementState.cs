using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards.MovementState
{
    public abstract class CardBaseMovementState : CardBaseState
    {
        protected RectTransform _rectTransform;
        public Vector2 TargetPosition;
        public abstract void OnMouseDrag(IStateContext uncastController);
        public abstract void OnMouseUp(IStateContext uncastController);
    }
}
