namespace Assets._Scripts.StateMachines.Cards
{
    public class CardFollowingState : CardBaseState
    {
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);
            //Update position to snap correctly on previous
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);

        }

        public override void OnMouseDrag(IStateContext uncastController)
        {
            CastContext(uncastController);

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
