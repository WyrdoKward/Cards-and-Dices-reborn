using Assets._Scripts.Utilities.Enums;
using System;

namespace Assets._Scripts.Cards.Logic
{
    internal class FollowerLogic : CardLogic
    {
        public override ECardType CardType => ECardType.Follower;

        internal override Action GetReceipe()
        {
            if (!base.VerifyReceipe()) return null;

            return null;
        }
    }
}
