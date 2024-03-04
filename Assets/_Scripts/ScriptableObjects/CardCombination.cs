using Assets._Scripts.ScriptableObjects.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    /// <summary>
    /// Hold a combination and its output for crafts with specific cards
    /// </summary>
    [CreateAssetMenu(fileName = "New Craft", menuName = "Combination/New Craft")]
    internal class CardCombination : ScriptableObject
    {
        public List<BaseCardSO> inputCards; // Liste des cartes d'entrée pour la combinaison
        public List<BaseCardSO> outputCards; // Liste des cartes résultantes de la combinaison
    }
}
