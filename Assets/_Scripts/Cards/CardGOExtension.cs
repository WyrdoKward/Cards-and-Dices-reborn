﻿using Assets._Scripts.Cards.Common;
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
        /// <returns>True if the GO is of type ECardType</returns>
        public static bool Is(this GameObject gameObject, ECardType cardType)
        {
            if (gameObject == null) return false;

            return gameObject.GetComponent<CardLogic>().CardType == cardType;
        }

        public static BaseCardSO BaseCardSO(this GameObject gameObject)
        {
            if (gameObject == null) return null;

            return gameObject.GetComponent<CardController>().CardSO;
        }
    }
}
