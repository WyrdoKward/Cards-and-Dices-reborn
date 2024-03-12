using Assets._Scripts.Cards;
using Assets._Scripts.ScriptableObjects.Entities;
using Assets._Scripts.Utilities.Cache;
using System;
using UnityEngine;

namespace Assets._Scripts.GameData.CardsBehaviour.Pnj
{
    [ComponentIdentifierAttribute(Name = "JunkArtisan")]
    internal class JunkArtisan : PnjSpecificBehaviour
    {
        bool questGiven = false;
        public override Action GetSpecificCombination()
        {
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
    }
}
