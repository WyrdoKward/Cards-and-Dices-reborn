using Assets._Scripts.Cards.Common;

namespace Assets._Scripts.StateMachines.Cards
{
    internal class CardIdleState : CardBaseState
    {
        public override void Enter(IStateContext card)
        {

        }

        public override void Exit(IStateContext card)
        {

        }

        public override void HandleInput(IStateContext card)
        {
            var controller = (CardController)card;
        }

        public override void UpdateState(IStateContext card)
        {
            var controller = (CardController)card;
            //if (!controller.IsSelected)
            //controller.DragAndDropSystem.LerpThisTo(Quaternion.Euler(90, 0, 0));
        }
    }
}
