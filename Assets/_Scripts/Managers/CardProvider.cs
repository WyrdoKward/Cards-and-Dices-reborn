using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    internal class CardProvider : MonoBehaviour
    {
        public List<GameObject> AllCards = new();

        public void RegisterCardToGlobalList(GameObject card)
        {
            AllCards.Add(card);
        }

        public List<GameObject> GetAllCardsExcept(List<GameObject> notThisCards)
        {
            return AllCards.Except(notThisCards).ToList();
        }

        public GameObject GetARandomCardByName(string name)
        {
            var cards = AllCards.Where(c => c.name == name).ToList();
            if (cards.Count == 0) return null;

            var rnd = Random.Range(0, cards.Count);
            return cards[rnd];
        }
    }
}
