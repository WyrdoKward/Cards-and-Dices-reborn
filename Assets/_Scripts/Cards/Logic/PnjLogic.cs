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
            if (CardController.NextCardInStack.Is(ECardType.Follower))
            {
                return Talk;
            }

            return GetComponent<PnjSpecificBehaviour>()?.GetSpecificCombination();
        }

        public void Talk()
        {
            GetComponent<PnjSpecificBehaviour>()?.Talk();
        }
    }
}
