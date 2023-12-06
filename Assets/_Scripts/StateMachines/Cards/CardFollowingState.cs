using Assets._Scripts.Cards.Common;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards
{
    public class CardFollowingState : CardBaseState
    {
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);

            Debug.Log($"{cardController.gameObject} following {cardController.PreviousCardInStack}");
            //Update position to snap correctly on previous
            cardGO.transform.localScale = GlobalVariables.CardBiggerScale;
            cardController.GetComponent<CardDisplay>().FollowPreviousCard(cardController.PreviousCardInStack);

            //Embnarquer la suivante
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
            cardController.GetComponent<CardDisplay>().FollowPreviousCard(cardController.PreviousCardInStack);

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

            var previousCard = cardController.PreviousCardInStack;
            cardController.GetComponent<CardDisplay>().FollowPreviousCard(previousCard);

            if (previousCard.GetComponent<CardController>().currentState is CardIdleState)
                cardController.SwitchState(cardController.IdleState);
        }
    }
}
