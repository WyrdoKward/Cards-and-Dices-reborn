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

            Action action = cardGO.GetComponent<CardLogic>().FireActionForEndTimer;
            //var delay = cardController.CardSO.
            var delay = 10f;
            timer = new CardTimer(cardController.gameObject, action, delay);
        }

        public override void Exit(IStateContext uncastController)
        {
            CastContext(uncastController);
            //Destruction du timer
            timer.Destroy();

            //Disperser les cartes


        }

        public override void UpdateState(IStateContext uncastController)
        {
            CastContext(uncastController);
            var isTimerOver = timer.Update();
            if (isTimerOver)
                cardController.SwitchState(cardController.NoTimerState);
        }
    }
}
