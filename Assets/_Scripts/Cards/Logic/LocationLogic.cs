using Assets._Scripts.Utilities;
using Assets._Scripts.Utilities.Enums;
using System;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Cards.Logic
{
    internal class LocationLogic : CardLogic
    {
        public override ECardType CardType => ECardType.Location;

        internal override Action GetReceipe()
        {
            if (!base.VerifyReceipe()) return null;

            if (StackHelper.GetCardsAboveInStack(gameObject).Count == 2) //TODO Utiliser ca pour tester les changements de recette lorsqu'un timer est déjà déclenché
                return Explore2;

            if (IsExploration())
                return Explore;



            return null;
        }

        private bool IsExploration()
        {
            return StackHelper.GetCardsAboveInStack(gameObject).All(c => c.Is(ECardType.Follower));
        }

        private void Explore()
        {
            var followers = StackHelper.GetCardsAboveInStack(gameObject);
            Debug.Log($"{followers.Count} F are exploring {gameObject}...");
        }

        private void Explore2()
        {
            var followers = StackHelper.GetCardsAboveInStack(gameObject);
            Debug.Log($"Explore 2");
        }
    }
}
