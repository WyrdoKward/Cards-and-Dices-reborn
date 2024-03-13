using Assets._Scripts.Cards.Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Utilities
{
    internal static class StackHelper
    {
        /// <summary>
        /// Créé le chaînage des cartes pour constituer les stacks
        /// </summary>
        /// <param name="thisCard">La carte sujette à l'opération</param>
        /// <param name="previousCard">La carte en dessous</param>
        /// <param name="nextCard">La carte au dessus</param>
        /// <param name="overrideWithNulls">True pour vider les previous/next</param>
        internal static void UpdateCardStack(GameObject thisCard, GameObject previousCard, GameObject nextCard, bool overrideWithNulls)
        {
            var thisController = thisCard.GetComponent<CardController>();

            if (overrideWithNulls)
            {
                //Dereferences thisCard from the next/previous if they are set to null
                if (previousCard == null && thisController.PreviousCardInStack != null)
                    thisController.PreviousCardInStack.GetComponent<CardController>().SetNextCard(null);

                if (nextCard == null && thisController.NextCardInStack != null)
                    thisController.NextCardInStack.GetComponent<CardController>().SetPreviousCard(null);

                thisController.SetPreviousCard(previousCard);
                thisController.SetNextCard(nextCard);
            }

            if (previousCard != null)
            {
                thisController.SetPreviousCard(previousCard);
                thisController.PreviousCardInStack.GetComponent<CardController>().SetNextCard(thisCard);
            }

            if (nextCard != null)
            {
                thisController.SetNextCard(nextCard);
                thisController.NextCardInStack.GetComponent<CardController>().SetPreviousCard(thisCard); ;
            }
        }

        /// <summary>
        /// Broke relation between thisCard and the previous one
        /// </summary>
        internal static void ClearPrevious(GameObject thisCard)
        {
            var thisController = thisCard.GetComponent<CardController>();

            if (thisController.PreviousCardInStack != null)
                thisController.PreviousCardInStack.GetComponent<CardController>().SetNextCard(null);

            thisController.SetPreviousCard(null);
        }

        /// <summary>
        /// Fonction récursive qui utilise le chaînage des cartes pour constituer le stack
        /// Index correspond au sortingOrder
        /// </summary>
        /// <returns>Index[0] = La carte la plus en dessous</returns>
        internal static List<GameObject> GetFullStack(GameObject card)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Fonction récursive qui utilise le chaînage des cartes pour récupérer les cartes au dessus du GO en paramètre
        /// </summary>
        /// <returns>Tous les GO au dessus de "card"</returns>
        internal static List<GameObject> GetCardsAboveInStack(GameObject card)
        {
            var cardsAbove = new List<GameObject>();
            var nextCard = card.GetComponent<CardController>().NextCardInStack;

            if (nextCard == null)
                return cardsAbove;

            cardsAbove.Add(nextCard);
            cardsAbove.AddRange(GetCardsAboveInStack(nextCard));

            return cardsAbove;
        }

        /// <summary>
        /// Retourne la carte la plus au dessus du stack auquel appartient la carte en paramètre
        /// </summary>
        internal static GameObject GetLastCardOfStack(GameObject card)
        {
            var nextCard = card.GetComponent<CardController>().NextCardInStack;

            if (nextCard == null)
                return card;

            return StackHelper.GetLastCardOfStack(nextCard);
        }


        /// <summary>
        /// Retourne la carte la plus proche d ela table du stack auquel appartient la carte en paramètre
        /// </summary>
        internal static GameObject GetFirstCardOfStack(GameObject card)
        {
            var previousCard = card.GetComponent<CardController>().PreviousCardInStack;

            if (previousCard == null)
                return card;

            return StackHelper.GetFirstCardOfStack(previousCard);
        }

        internal static int ComputeOrderInLayer(GameObject card)
        {
            var previousCard = card.GetComponent<CardController>().PreviousCardInStack;
            if (previousCard == null)
                return 0;


            var previousSortingOrder = ComputeOrderInLayer(previousCard);
            return previousSortingOrder + 1;

        }
    }
}
