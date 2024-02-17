using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Systems.Timer;
using Assets._Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards.TimerState
{
    public class CardRunningState : CardBaseTimerState
    {
        public Action EndTimerAction;
        private CardTimer timer;



        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);
            Debug.Log($"{cardGO} running...");
            cardGO.GetComponent<Canvas>().sortingOrder = StackHelper.ComputeOrderInLayer(cardGO);

            //var delay = cardController.CardSO.
            if (EndTimerAction == null)
                Debug.LogError($"EndTimerAction null pour {cardGO} !");

            var delay = 10f;
            timer = new CardTimer(cardGO, EndTimerAction, delay);
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
            Debug.Log($"{cardGO} exit running");
            EndTimerAction = null;
            timer.Destroy(cardGO);
            DisperseCards();
        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
            var isTimerOver = timer.Update();
            if (isTimerOver)
            {
                cardController.SwitchState(cardController.NoTimerState);
            }

            if (cardController.GetComponent<CardLogic>().GetReceipe() == null)
                cardController.SwitchState(cardController.NoTimerState);

        }

        private void DisperseCards()
        {
            var cardsToDisperse = StackHelper.GetCardsAboveInStack(cardGO);
            cardController.UnlinkNextCard();
            cardsToDisperse.ForEach(c => c.Disperse());
        }
    }
}
