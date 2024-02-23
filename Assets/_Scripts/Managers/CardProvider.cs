using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    internal class CardProvider : MonoBehaviour
    {
        public List<GameObject> allCards = new List<GameObject>();

        public void RegisterCardToGlobalList(GameObject card)
        {
            allCards.Add(card);
        }

        public List<GameObject> GetAllCardsExcept(List<GameObject> notThisCards)
        {
            return allCards.Except(notThisCards).ToList();
        }
    }
}
