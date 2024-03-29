﻿using Assets._Scripts.Cards.Common;
using Assets._Scripts.Utilities;

namespace Assets._Scripts.StateMachines.Cards.MovementState
{
    public class CardFollowingState : CardBaseMovementState
    {
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);

            //Debug.Log($"{cardController.gameObject} following {cardController.PreviousCardInStack}");
            //Update position to snap correctly on previous
            cardGO.transform.localScale = GlobalVariables.CardBiggerScale;
            cardController.GetComponent<CardDisplay>().FollowPreviousCard(cardController.PreviousCardInStack);

            //Embarquer la suivante
            cardController.NextCardInStack?.Follow();
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

            var previousCard = cardController.PreviousCardInStack;

            if (previousCard == null)
            {
                cardController.SwitchState(cardController.IdleState);
                return;
            }

            cardController.GetComponent<CardDisplay>().FollowPreviousCard(previousCard);

            if (previousCard.GetComponent<CardController>().currentMovementState is CardIdleState)
                cardController.SwitchState(cardController.IdleState);
        }
    }
}
