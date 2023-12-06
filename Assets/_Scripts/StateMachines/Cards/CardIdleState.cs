using Assets._Scripts.Cards.Common;
using Assets._Scripts.Utilities;

namespace Assets._Scripts.StateMachines.Cards
{
    public class CardIdleState : CardBaseState
    {
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);
            cardGO.transform.localScale = GlobalVariables.CardDefaultScale;

            //if next => switch thenm to follow
            var nextCard = cardController.NextCardInStack;
            if (nextCard != null)
            {
                var nextController = nextCard.GetComponent<CardController>();
                nextController.SwitchState(nextController.IdleState);
            }
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
        }

        public override void OnMouseDrag(IStateContext uncastController)
        {
            CastContext(uncastController);
            //TODO Ajouter contrôle de mvt minimum
            cardController.SwitchState(cardController.MovingState);
            //// Passer toutes les autres cartes au dessus dans le stack en following
            ///// Useless, c'est fait dans moving.Enter
            //foreach (var card in StackHelper.GetCardsAboveInStack(cardGO))
            //{
            //    var nextController = card.GetComponent<CardController>();
            //    nextController.SwitchState(nextController.FollowingState);
            //}
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
