using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Systems.Timer;
using Assets._Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards.TimerState
{
    public class CardRunningState : CardBaseTimerState
    {
        public float? TimerDuration;
        public Action EndTimerAction;
        private CardTimer timer;

        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);
            cardGO.GetComponent<Canvas>().sortingOrder = StackHelper.ComputeOrderInLayer(cardGO);

            InitTimer();
            //Debug.Log($"[CardRunningState] {cardGO} running with {EndTimerAction.Method}");
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
            //Debug.Log($"[CardRunningState] {cardGO} exit running {EndTimerAction.Method}");
            EndTimerAction = null;
            timer.Destroy(cardGO);
            DisperseCards();
        }

        /// <summary>
        /// Resets the timer and link a new action without dispersing cards
        /// </summary>
        /// <param name="newAction"></param>
        public void ResetState(Action newAction)
        {
            timer.Destroy(cardGO);
            EndTimerAction = newAction;
            InitTimer();
        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
            var isTimerOver = timer.Update();
            var isCurrentStackACombination = cardGO.GetComponent<CardLogic>().GetActionToExecuteAfterTimer() != null;
            if (!isCurrentStackACombination || isTimerOver)
            {
                cardController.SwitchState(cardController.NoTimerState);
            }
        }

        private void InitTimer()
        {
            if (EndTimerAction == null)
            {
                Debug.LogError($"EndTimerAction null pour {cardGO} !");
                return;
            }

            timer = new CardTimer(cardGO, EndTimerAction, TimerDuration ?? GlobalVariables.DefaultTimerDuration);
        }

        private void DisperseCards()
        {
            var cardsToDisperse = StackHelper.GetCardsAboveInStack(cardGO);
            cardController.UnlinkNextCard();
            cardsToDisperse.ForEach(c => c.Disperse());
        }
    }
}
