using Assets._Scripts.Cards.Logic;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Location", menuName = "Card/Location")]
    internal class LocationCardSO : BaseCardSO
    {
        public float ExplorationTime;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<LocationLogic>();
        }
    }
}
