using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Utilities.Enums;
using System;
using UnityEngine;

namespace Assets._Scripts.Utilities.StaticHelpers
{
    internal static class CardUtility
    {
        internal static ECardType GetCardType(GameObject gameObject)
        {
            var componentName = gameObject.GetComponent<CardLogic>().GetType().Name.Replace("Logic", "");

            if (!Enum.TryParse<ECardType>(componentName, out var cardType))
                throw new Exception($"Type de carte {componentName} inconnu");

            return cardType;
        }
    }
}
