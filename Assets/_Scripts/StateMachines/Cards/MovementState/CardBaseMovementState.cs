namespace Assets._Scripts.StateMachines.Cards.MovementState
{
    public abstract class CardBaseMovementState : CardBaseState
    {
        public abstract void OnMouseDrag(IStateContext uncastController);
        public abstract void OnMouseUp(IStateContext uncastController);
    }
}
