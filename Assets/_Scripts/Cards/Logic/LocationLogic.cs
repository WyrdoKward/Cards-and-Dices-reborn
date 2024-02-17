using Assets._Scripts.Utilities;
using System;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Cards.Logic
{
    internal class LocationLogic : CardLogic
    {
        internal override Action GetReceipe()
        {
            if (!base.VerifyReceipe()) return null;

            if (StackHelper.GetCardsAboveInStack(gameObject).All(c => c.GetComponent<CardLogic>() is FollowerLogic))
                return Explore;


            return null;
        }

        private void Explore()
        {
            var followers = StackHelper.GetCardsAboveInStack(gameObject);
            Debug.Log($"{followers.Count} F are exploring {gameObject}...");
        }
    }
}
