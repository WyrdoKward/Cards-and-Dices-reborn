using Assets._Scripts.Cards.Common;
using Assets._Scripts.Managers;
using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Systems
{
    internal class CardSpawner : MonoBehaviour
    {
        public GameObject CardPrefab;
        public GameObject CardContainerGO;
        public BaseCardSO DefaultLoot;

        private CardProvider cardProvider;

        private void Start()
        {
            cardProvider = GameObject.Find("Managers/CardManager").GetComponent<CardProvider>();
        }

        public void GenerateRandomCardFromList(List<BaseCardSO> cards)
        {
            var filteredLoot = FilterOutExistingUniqueCards(cards);

            if (filteredLoot.Count == 0)
                SpawnCard(DefaultLoot);
            else
            {
                var rnd = Random.Range(0, filteredLoot.Count);
                var chosenCard = filteredLoot[rnd];
                SpawnCard(chosenCard);
            }
        }

        private void SpawnCard(BaseCardSO cardData)
        {
            Debug.Log($"SpawnCard {cardData.name}");
            var controller = CardPrefab.GetComponentInChildren<CardController>();
            controller.CardSO = cardData;

            var spawedCardGO = Instantiate(CardPrefab, new Vector3(10f, 10f, 0), Quaternion.identity);

            spawedCardGO.name = cardData.name;
            spawedCardGO.transform.SetParent(CardContainerGO.transform, false);
            spawedCardGO.GetComponentInChildren<RectTransform>().localScale = GlobalVariables.CardDefaultScale;
            spawedCardGO.GetComponent<Canvas>().sortingOrder = 10; // temporaire, le temps de coder un truc pour capter les cartes autour et juste se poser au dessus
        }

        private List<BaseCardSO> FilterOutExistingUniqueCards(IEnumerable<BaseCardSO> cards)
        {
            var res = new List<BaseCardSO>(cards);

            var inGameCardNames = cardProvider.AllCards.Select(c => c.name);

            foreach (var cardSO in cards)
            {
                if (!cardSO.IsUnique)
                    continue;

                if (inGameCardNames.Contains(cardSO.Name))
                    res.Remove(cardSO);
            }
            return res;
        }
    }
}
