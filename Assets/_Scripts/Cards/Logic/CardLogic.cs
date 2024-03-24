using Assets._Scripts.Cards.Common;
using Assets._Scripts.ScriptableObjects.Entities;
using Assets._Scripts.Systems;
using Assets._Scripts.Utilities.Enums;
using System;
using UnityEngine;

namespace Assets._Scripts.Cards.Logic
{
    internal abstract class CardLogic : MonoBehaviour
    {
        public abstract ECardType CardType { get; }
        internal CardSpawner CardSpawner;
        internal CardController CardController;

        public void Start()
        {
            CardController = GetComponent<CardController>();
            CardSpawner = GameObject.Find("Managers/CardManager").GetComponent<CardSpawner>();
        }

        /// <summary>
        /// Easy access to the SO held by the controller
        /// </summary>
        internal BaseCardSO ScriptableObject()
        {
            return GetComponent<CardController>().CardSO;
        }

        /// <summary>
        ///  Check prérequis généraux avant de déterminer une recette
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        internal bool VerifyReceipe()
        {
            //Do not call this on a card not first in its stack
            if (GetComponent<CardController>().PreviousCardInStack != null)
            {
                Debug.LogWarning("Are you sure you need to call this on a card not first in its stack ?");
                return false;
            }

            // Si il n'y a pas de carte suivante
            if (GetComponent<CardController>().NextCardInStack == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Renvoie la méthode qui correspond à la combinaison de cartes stackées
        /// A apeller sur la PREMIERE carte du stack
        /// </summary>
        /// <returns></returns>
        internal virtual Action GetActionToExecuteAfterTimer()
        {
            Debug.LogWarning("Are you sure you need to call this on the base class ? Maybe you forgot to implement it on the derived class.");
            return null;
        }
    }
}
