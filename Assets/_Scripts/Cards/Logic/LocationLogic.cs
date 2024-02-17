using Assets._Scripts.Cards.Common;
using System;
using UnityEngine;

namespace Assets._Scripts.Cards.Logic
{
    internal class LocationLogic : CardLogic
    {
        internal override Action GetReceipe()
        {
            if (!base.VerifyReceipe()) return null;

            if (GetComponent<CardController>().NextCardInStack.GetComponent<CardLogic>() is FollowerLogic)
                return Explore;

            return null;
        }

        private void Explore()
        {
            Debug.Log($"Exploring {gameObject}...");
        }
    }
}
