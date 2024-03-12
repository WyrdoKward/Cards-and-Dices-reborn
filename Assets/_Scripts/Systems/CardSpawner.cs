using Assets._Scripts.Cards.Common;
using Assets._Scripts.Managers;
using Assets._Scripts.ScriptableObjects.Entities;
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

        public void SpawnCard(BaseCardSO cardData)
        {
            Debug.Log($"SpawnCard {cardData.Name}");
            var controller = CardPrefab.GetComponentInChildren<CardController>();
            controller.CardSO = cardData;

            //var spanwPosition = FindFreePositionNear(new Vector2(10f, 10f));
            var spawnedCardGO = Instantiate(CardPrefab, new Vector2(10f, 10f), Quaternion.identity);

            spawnedCardGO.name = cardData.Name;
            spawnedCardGO.transform.SetParent(CardContainerGO.transform, false);
            spawnedCardGO.GetComponentInChildren<RectTransform>().localScale = GlobalVariables.CardDefaultScale;
            spawnedCardGO.GetComponent<Canvas>().sortingOrder = 10; // temporaire, le temps de coder un truc pour capter les cartes autour et juste se poser au dessus

            //spawedCardGO.transform.position doit être entre +- 9 en x et +- 5 en y environ

            //var offsetX = go.GetComponent<RectTransform>().sizeDelta.x / 2;
            //var offsetY = go.GetComponent<RectTransform>().sizeDelta.y / 2;

            var offsetX = 100 / 2;
            var offsetY = 140 / 2;

            spawnedCardGO.transform.position = GetRandomSpawnPosition(offsetX, offsetY);
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

        public List<Vector2> PositionsToVisualize; // Vecteurs cibles à visualiser

        private void OnDrawGizmos()
        {
            // Dessiner un gizmo pour chaque position ciblée
            Gizmos.color = Color.red;
            foreach (var position in PositionsToVisualize)
            {
                Gizmos.DrawSphere(position, 0.1f); // Changer 0.1f pour ajuster la taille du gizmo
            }
        }

        Vector2 GetRandomSpawnPosition(float offsetX, float offsetY)
        {
            var canvas = CardContainerGO.GetComponentInParent<Canvas>();

            var rangeX = canvas.pixelRect.width - offsetX;
            var rangeY = canvas.pixelRect.height - offsetY;

            var i = 0;
            // Générer une position aléatoire jusqu'à ce qu'une position vide soit trouvée
            while (i < 1000)
            {
                i++;
                var randomScreenPosition = new Vector2(Random.Range(offsetX, rangeX), Random.Range(offsetY, rangeY));

                // Convertir la position de l'écran à la position du monde (pour le Canvas en mode "Screen Space Camera")
                var randomWorldPosition = Camera.main.ScreenToWorldPoint(randomScreenPosition);
                //PositionsToVisualize.Add(randomWorldPosition);

                //if (DragAndDropHelper.FindOverlappedCardIfExists(go) == null)
                if (!cardProvider.IsPositionOccupiedByCard(randomWorldPosition))
                {
                    return randomWorldPosition;
                }
            }
            return Vector2.zero;
        }
    }
}
