using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    internal class CardProvider : MonoBehaviour
    {
        public List<GameObject> AllCards = new();
        public List<Vector2> OccupiedPositions = new();
        private GameObject StandardCard = null; //Carte témoin si on besoin de récupérer des éléments propres à toutes les cartes

        public void RegisterCardToGlobalList(GameObject card)
        {
            if (StandardCard = null)
                StandardCard = card;

            AllCards.Add(card);
            OccupiedPositions.Add(card.transform.position);
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

        public bool IsPositionOccupiedByCard(Vector2 position)
        {
            foreach (Vector3 occupiedPosition in OccupiedPositions)
            {
                //if (Vector3.Distance(position, occupiedPosition) < StandardCard.GetComponent<RectTransform>().sizeDelta.y)
                if (Vector3.Distance(position, occupiedPosition) < 1.40f) //on compare la distance à la hauteur d'une carte
                {
                    Debug.Log($"Position occupée : {position}");
                    return true;
                }
            }
            return false;
        }

        public void Remove(GameObject card)
        {
            if (card == null) return;

            if (AllCards.Contains(card))
                AllCards.Remove(card);
        }
    }
}
