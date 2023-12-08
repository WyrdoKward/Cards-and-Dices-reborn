using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Utilities;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Cards
{
    public class CardRunningState : CardBaseState
    {
        public override void Enter(IStateContext uncastController)
        {
            CastContext(uncastController);
            cardGO.GetComponent<Canvas>().sortingOrder = StackHelper.ComputeOrderInLayer(cardGO);

            LaunchActionWithTimer();
        }

        public override void Exit(IStateContext uncastController)
        {
        }

        public override void OnMouseDrag(IStateContext uncastController)
        {
            CastContext(uncastController);
            //TODO Ajouter contrôle de mvt minimum
            //cardController.SwitchState(cardController.MovingState); //TODO SI on veut bouger, il faut gérer runningState et Movingstate en meme temps...
        }

        public override void OnMouseUp(IStateContext uncastController)
        {
        }

        public override void UpdateState(IStateContext uncastController)
        {
        }

        private void LaunchActionWithTimer()
        {
            cardGO.GetComponent<CardLogic>().LaunchActionWthTimer();
        }
    }
}
