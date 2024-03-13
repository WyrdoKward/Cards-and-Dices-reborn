using Assets._Scripts.Cards.Common;
using Assets._Scripts.Cards.Logic;
using Assets._Scripts.ScriptableObjects.Entities;
using Assets._Scripts.Utilities.Enums;
using UnityEngine;

namespace Assets._Scripts.Cards
{
    /// <summary>
    /// Extention class to ease logic access via the game object
    /// </summary>
    internal static class CardGOExtension
    {
        /// <returns>Compare name and card type</returns>
        public static bool Is(this GameObject gameObject, ECardType cardType, string name = null)
        {
            if (gameObject == null) return false;

            if (gameObject.GetComponent<CardLogic>().CardType != cardType)
                return false;

            if (string.IsNullOrEmpty(name) || name == gameObject.GetComponent<CardController>().CardSO.Name)
                return true;

            return false;
        }

        public static BaseCardSO BaseCardSO(this GameObject gameObject)
        {
            if (gameObject == null) return null;

            return gameObject.GetComponent<CardController>().CardSO;
        }

        public static void TransformInto(this GameObject gameObject, BaseCardSO newCard)
        {
            if (gameObject == null) return;

            gameObject.GetComponent<CardController>().ReloadCard(newCard);
        }
    }
}
