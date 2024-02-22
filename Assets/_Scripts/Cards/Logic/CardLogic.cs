using Assets._Scripts.Cards.Common;
using Assets._Scripts.Utilities.Enums;
using System;
using UnityEngine;

namespace Assets._Scripts.Cards.Logic
{
    internal abstract class CardLogic : MonoBehaviour
    {
        public abstract ECardType CardType { get; }
        /// <summary>
        ///  Check prérequis généraux avant de déterminer une recette
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        internal virtual bool VerifyReceipe()
        {
            if (GetComponent<CardController>().PreviousCardInStack != null)
                throw new System.Exception("Do not call this on a card not first in its stack");

            // Si il n'y a pas de carte suivante
            if (GetComponent<CardController>().NextCardInStack == null)
            {
                return false;
            }

            return true;
        }

        internal virtual Action GetReceipe()
        {
            return null;
        }
    }
}
