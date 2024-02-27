using Assets._Scripts.Managers;
using Assets._Scripts.Utilities.Cache;
using UnityEngine;

namespace Assets._Scripts.GameData.CardsBehaviour
{
    /// <summary>
    /// Specific behaviour for a particular card
    /// Toutes les implémentations de cette classe doivent positionne l'attribut "ComponentIdentifierAttribute"
    /// </summary>
    internal abstract class BaseCardBehaviour : MonoBehaviour
    {
        protected CardProvider CardProvider;

        private void Awake()
        {
            CardProvider = GameObject.Find("Managers/CardManager").GetComponent<CardProvider>();
        }

        private void Start()
        {
            if (!this.GetType().IsDefined(typeof(ComponentIdentifierAttribute), false))
            {
                Debug.LogError($"ComponentIdentifierAttribute not defined");
            }

            //Vérifier aussi que la valeur de ComponentIdentifierAttribute existe dans ComponentCache si c'est pas trop tot ici
        }
    }
}
