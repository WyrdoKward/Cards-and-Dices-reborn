using UnityEngine;

namespace Assets._Scripts.Utilities
{
    internal static class GlobalVariables
    {

        private static float cardElementsScaleInt = 0.42552f;
        public static Vector3 CardElementsScale = new Vector3(cardElementsScaleInt, cardElementsScaleInt, cardElementsScaleInt);

        #region CardTypes
        public static readonly Color FOLLOWER_DefaultSliderColor = Color.cyan;
        public static readonly Color LOCATION_DefaultSliderColor = Color.green;
        public static readonly Color THREAT_DefaultSliderColor = Color.red;
        public static readonly Color RESOURCE_DefaultSliderColor = Color.white;
        public static readonly Color PNJ_DefaultSliderColor = Color.magenta;
        #endregion

        #region animationUI
        public static readonly float DefaultLerpingValue = 5f;
        public const float LerpSpeed = 25f;
        public static readonly float CardDragNDropScaleFactor = 1.05f;
        public static readonly float CardOffsetOnSnap = 15f;

        //Canvas 
        public static readonly int DefaultCardSortingLayer = 0; //Default layer for a card just laying on the table
        public static readonly int OnDragCardSortingLayer = 99; //Allows to put the current dragged card above everything else
        #endregion
    }
}
