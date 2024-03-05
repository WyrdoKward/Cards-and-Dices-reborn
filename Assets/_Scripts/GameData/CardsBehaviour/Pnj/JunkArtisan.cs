using Assets._Scripts.Cards;
using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.ScriptableObjects.Entities;
using Assets._Scripts.Utilities;
using Assets._Scripts.Utilities.Cache;
using System;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.GameData.CardsBehaviour.Pnj
{
    [ComponentIdentifierAttribute(Name = "JunkArtisan")]
    internal class JunkArtisan : PnjSpecificBehaviour
    {
        private CardCombinationSO currentCardCombinationSO;
        public override Action GetSpecificCombination()
        {
            var stack = StackHelper.GetCardsAboveInStack(gameObject);
            var scriptablesObjects = stack.Select(c => c.BaseCardSO());

            var combinations = ((PnjCardSO)gameObject.BaseCardSO()).CardCombinations;

            foreach (var combination in combinations)
            {
                var i = 0;
                var inputs = combination.InputCards;
                if (scriptablesObjects.Count() != inputs.Count)
                {
                    i += 1;
                    continue;
                }

                //TODO refactoriser cette logique avec le EInteractionType ?
                if (scriptablesObjects.OrderBy(c => c.Name).SequenceEqual(inputs.OrderBy(c => c.Name)))
                {
                    currentCardCombinationSO = combination;
                    if (combination.InteractionType == Utilities.Enums.EInteractionType.Trade)
                        return Trade;
                }


            }


            return null;
        }

        public override void Talk()
        {
            Debug.Log($"Hello, I'm Junk Artisan !");
        }

        public override void Trade()
        {
            Debug.Log($"Trading ({currentCardCombinationSO.InputCards.Count} for {currentCardCombinationSO.OutputCards.Count})");
            //Delete input cards in stack

            //Spawn Output cards
        }
    }
}
