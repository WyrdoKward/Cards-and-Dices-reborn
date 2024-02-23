using UnityEngine;

namespace Assets._Scripts.Utilities
{
    internal static class GlobalVariables
    {
        #region CardTypes
        public static readonly Color FOLLOWER_DefaultSliderColor = Color.cyan;
        public static readonly Color LOCATION_DefaultSliderColor = Color.green;
        public static readonly Color THREAT_DefaultSliderColor = Color.red;
        public static readonly Color RESOURCE_DefaultSliderColor = Color.white;
        public static readonly Color PNJ_DefaultSliderColor = Color.magenta;
        #endregion

        #region animationUI
        public const float DragThresholdDelta = 0.1f;
        public static readonly float DefaultLerpingValue = 5f;
        public const float LerpSpeed = 25f;
        public static readonly float CardDragNDropScaleFactor = 1.05f;
        public static readonly Vector3 CardDefaultScale = new Vector3(1, 1, 1);
        public static readonly Vector3 CardBiggerScale = new Vector3(1.05f, 1.05f, 1.05f);
        public static readonly float CardOffsetOnSnap = 15f;

        //Colliders
        public static readonly float CardSizeY = 140f;
        public static readonly float SnapOffsetY = 62.5f;


        //Canvas 
        public static readonly int DefaultCardSortingLayer = 0; //Default layer for a card just laying on the table
        public static readonly int OnDragCardSortingLayer = 99; //Allows to put the current dragged card above everything else
        #endregion
    }
}
