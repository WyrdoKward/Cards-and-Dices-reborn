using Assets._Scripts.GameData.CardsBehaviour;
using Assets._Scripts.Utilities.Enums;
using System;

namespace Assets._Scripts.Cards.Logic
{
    internal class PnjLogic : CardLogic
    {
        public override ECardType CardType => ECardType.Pnj;

        internal override Action GetActionToExecuteAfterTimer()
        {
            //Déplacer dans GetSpecificCombination et le ronommer ?
            if (CardController.NextCardInStack.Is(ECardType.Follower))
            {
                GetComponent<PnjSpecificBehaviour>()?.Talk();
            }

            return GetComponent<PnjSpecificBehaviour>()?.GetSpecificCombination();
        }
    }
}
