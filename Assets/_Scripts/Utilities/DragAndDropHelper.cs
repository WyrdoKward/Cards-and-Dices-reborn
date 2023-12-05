using Assets._Scripts.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Utilities
{
    internal static class DragAndDropHelper
    {
        public static GameObject FindOverlappedCardIfExists(GameObject movedCard)
        {
            var currentStack = new List<GameObject>() { movedCard };
            currentStack.AddRange(StackHelper.GetCardsAboveInStack(movedCard));
            GameObject foundCard = null;
            foreach (var card in GameObject.Find("CardManager").GetComponent<CardManager>().GetAllCardsExcept(currentStack))
            {
                if (card.GetComponent<BoxCollider2D>().bounds.Intersects(movedCard.GetComponent<BoxCollider2D>().bounds))
                {
                    foundCard = card;
                    break;
                }
            }

            if (foundCard == null)
                return null;

            // Determiner la carte la plus haute du stack pour en faire la target à se snapper dessus
            return StackHelper.GetLastCardOfStack(foundCard);
        }
    }
}
