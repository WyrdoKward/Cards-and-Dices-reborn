using Assets._Scripts.GameData.CardsBehaviour;
using Assets._Scripts.Utilities.Enums;
using System;

namespace Assets._Scripts.Cards.Logic
{
    internal class ThreatLogic : CardLogic
    {
        public override ECardType CardType => ECardType.Threat;

        internal override Action GetActionToExecuteAfterTimer()
        {
            if (!base.VerifyReceipe()) return null;

            if (CardController.NextCardInStack == null)
                return ExecuteThreatAfterTimer;

            return null;
        }

        public void ExecuteThreatAfterTimer()
        {
            GetComponent<ThreatSpecificBehaviour>()?.ExecuteThreat();
        }
    }
}
