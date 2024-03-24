using Assets._Scripts.Cards.Common;
using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Utilities;
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

        public List<List<GameObject>> GetAllStacksOrSingleCards()
        {
            var res = new List<List<GameObject>>();
            //var processedCards = new HashSet<GameObject>(); //Si pblms de perfs: ne pas faire le FindAll mais utiliser cette liste pour stocker les cartes déjà traitées au fur et à mesure et sortir de la boucle directmeent

            var allCardsWithoutPrevious = AllCards.FindAll(card => card.GetComponent<CardController>().PreviousCardInStack == null);
            foreach (var card in allCardsWithoutPrevious)
            {
                //On ne prend que les cartes à la racine et on cherche leur stack
                res.Add(StackHelper.GetFullStack(card));
            }

            return res;
        }

        //TODO tester le comportement aussi avec les cartes seules !!
        public List<GameObject> GetAllCardsThatInteractsWith(GameObject cardDragged)
        {
            var res = new List<GameObject>();
            var allStacks = GetAllStacksOrSingleCards();

            foreach (var stackOrSingleCard in allStacks)
            {
                // Si des cartes sont seules ou stackées sur le board :
                // SetNext temporairement sur celle du dessus pour imiter le chainage
                // GetActionToExecuteAfterTimer sur celle à la base

                var firstCard = stackOrSingleCard.First();
                var lastCard = stackOrSingleCard.Last();

                lastCard.GetComponent<CardController>().SetNextCard(cardDragged);
                var action = firstCard.GetComponent<CardLogic>().GetActionToExecuteAfterTimer();
                if (action != null)
                    res.Add(firstCard);

                lastCard.GetComponent<CardController>().SetNextCard(null);
            }

            return res;
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
