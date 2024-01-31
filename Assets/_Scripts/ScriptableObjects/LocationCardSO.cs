using Assets._Scripts.Cards.Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Location", menuName = "Card/Location")]
    internal class LocationCardSO : BaseCardSO
    {
        public float ExplorationTime;
        public List<BaseCardSO> Loot;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<LocationLogic>();
        }
    }
}
