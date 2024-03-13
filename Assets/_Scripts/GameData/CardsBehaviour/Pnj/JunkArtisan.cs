using Assets._Scripts.Cards;
using Assets._Scripts.ScriptableObjects.Entities;
using Assets._Scripts.Utilities;
using Assets._Scripts.Utilities.Cache;
using Assets._Scripts.Utilities.Enums;
using System;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.GameData.CardsBehaviour.Pnj
{
    [ComponentIdentifierAttribute(Name = "JunkArtisan")]
    internal class JunkArtisan : PnjSpecificBehaviour
    {
        bool questGiven = false;
        private GameObject questObject;

        public override Action GetSpecificCombination()
        {
            //Actions spécifiques à cette classe
            var cardsAbove = StackHelper.GetCardsAboveInStack(gameObject);
            var firstCard = cardsAbove.First();

            if (cardsAbove.Count == 1 && firstCard.Is(ECardType.Resource, "Quest Object"))
            {
                questObject = firstCard;
                return TransformIntoFollower;
            }

            //Si aucune, on cherche une action spécifiques à son type (PnjSpecificBehaviour)
            return base.GetSpecificCombination();
        }

        public override void Talk()
        {
            base.Talk();
            Debug.Log($"Hello, I'm Junk Artisan and you can sell me Scrap Metal !");
            if (!questGiven)
            {
                Debug.Log($"Do you want to go on an adventure ?");
                var questSO = ((PnjCardSO)gameObject.BaseCardSO()).Quest;
                if (questSO != null)
                {
                    CardSpawner.SpawnCard(questSO);
                    questGiven = true;
                }
            }
            else
            {
                Debug.Log($"Where are you with my quest ?");
            }
        }

        protected override void Trade()
        {
            base.Trade();
        }

        private void TransformIntoFollower()
        {
            Destroy(questObject);
            questGiven = false;
            gameObject.TransformInto(gameObject.BaseCardSO().OtherForms[0]);
            Debug.Log("I'll follow you !");
        }
    }
}
