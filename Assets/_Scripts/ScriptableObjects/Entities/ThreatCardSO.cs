using Assets._Scripts.Cards.Logic;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects.Entities
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Threat", menuName = "Card/Threat")]
    internal class ThreatCardSO : BaseCardSO
    {
        public float ExecuteThreatDuration = 10f;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<ThreatLogic>();
        }
    }
}
