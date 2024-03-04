using Assets._Scripts.Cards.Logic;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects.Entities
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Pnj", menuName = "Card/Pnj")]
    internal class PnjCardSO : BaseCardSO
    {
        public float TalkDuration = 10f;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);
            cardBodyGO.AddComponent<PnjLogic>();
        }
    }
}
