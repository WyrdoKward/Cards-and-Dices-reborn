using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Systems.Timer;
using Assets._Scripts.Utilities;
using System;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards.TimerState
{
    public class CardRunningState : CardBaseTimerState
    {
        private CardTimer timer;

        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);
            cardGO.GetComponent<Canvas>().sortingOrder = StackHelper.ComputeOrderInLayer(cardGO);

            Action action = cardGO.GetComponent<CardLogic>().LaunchActionWthTimer;
            //var delay = cardController.CardSO.
            var delay = 3f;
            timer = new CardTimer(action, delay);

            LaunchActionWithTimer();
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
            //Destruction du timer

            //Disperser les cartes


        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
            var isTimerOver = timer.Update();
        }

        private void LaunchActionWithTimer()
        {
            cardGO.GetComponent<CardLogic>().LaunchActionWthTimer();
        }
    }
}
