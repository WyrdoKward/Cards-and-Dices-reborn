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
                if (Intersects2D(card, movedCard))
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

        private static bool Intersects2D(GameObject card1, GameObject card2)
        {
            var collider1 = card1.GetComponent<BoxCollider2D>();
            var collider2 = card2.GetComponent<BoxCollider2D>();

            var bounds1 = collider1.bounds;
            var bounds2 = collider2.bounds;

            // Ignorer la position Z en ajustant les valeurs min et max
            bounds1.min = new Vector3(bounds1.min.x, bounds1.min.y, 0f);
            bounds1.max = new Vector3(bounds1.max.x, bounds1.max.y, 0f);

            bounds2.min = new Vector3(bounds2.min.x, bounds2.min.y, 0f);
            bounds2.max = new Vector3(bounds2.max.x, bounds2.max.y, 0f);

            return bounds1.Intersects(bounds2);
        }
    }
}
