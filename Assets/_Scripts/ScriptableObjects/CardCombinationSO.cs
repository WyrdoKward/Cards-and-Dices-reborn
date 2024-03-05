using Assets._Scripts.ScriptableObjects.Entities;
using Assets._Scripts.Utilities.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    /// <summary>
    /// Hold a combination and its output for crafts with specific cards
    /// </summary>
    [CreateAssetMenu(fileName = "New Craft", menuName = "Combination/New Craft")]
    internal class CardCombinationSO : ScriptableObject
    {
        public EInteractionType InteractionType;
        public List<BaseCardSO> InputCards; // Liste des cartes d'entrée pour la combinaison
        public List<BaseCardSO> OutputCards; // Liste des cartes résultantes de la combinaison
    }
}
