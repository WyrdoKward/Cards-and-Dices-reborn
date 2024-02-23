using Assets._Scripts.Utilities.Enums;
using System;

namespace Assets._Scripts.Cards.Logic
{
    internal class ResourceLogic : CardLogic
    {
        public override ECardType CardType => ECardType.Resource;

        internal override Action GetReceipe()
        {
            if (!base.VerifyReceipe()) return null;

            return null;
        }
    }
}
