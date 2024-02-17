using System;

namespace Assets._Scripts.Cards.Logic
{
    internal class FollowerLogic : CardLogic
    {
        internal override Action GetReceipe()
        {
            if (!base.VerifyReceipe()) return null;

            return null;
        }
    }
}
