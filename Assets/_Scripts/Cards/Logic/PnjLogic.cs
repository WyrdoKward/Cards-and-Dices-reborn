﻿using Assets._Scripts.Utilities.Enums;
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

            return null;
        }

        public void Talk()
        {

        }
    }
}
