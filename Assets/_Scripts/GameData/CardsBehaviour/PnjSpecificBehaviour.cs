using Assets._Scripts.Cards;
using Assets._Scripts.GameData.CardsBehaviour.Actions;
using Assets._Scripts.ScriptableObjects;
using Assets._Scripts.ScriptableObjects.Entities;
using Assets._Scripts.Utilities;
using System;
using System.Linq;

namespace Assets._Scripts.GameData.CardsBehaviour
{
    internal abstract class PnjSpecificBehaviour : BaseCardBehaviour
    {
        //Méthodes virtuelles dans PnjSpecificBehaviour pour le moment. Voir si pertinent de les remonter dans BaseCardBehaviour
        private CardCombinationSO currentCardCombinationSO;

        /// <summary>
        /// Retourne l'action correspondante à la combinaison de carte stackées sur l'objet si correspondance
        /// </summary>
        /// <returns></returns>
        public virtual Action GetSpecificCombination()
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

        public virtual void Talk()
        {
        }

        [ActionName("Un échange est possible...")]
        protected virtual void Trade()
        {
            //Delete input cards in stack
            foreach (var card in StackHelper.GetCardsAboveInStack(gameObject))
            {
                Destroy(card);
            }

            //Spawn Output cards
            foreach (var card in currentCardCombinationSO.OutputCards)
            {
                CardSpawner.SpawnCard(card);
            }
        }
    }
}
