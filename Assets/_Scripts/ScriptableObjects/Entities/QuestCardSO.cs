using Assets._Scripts.Cards.Logic;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects.Entities
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Quest", menuName = "Card/Quest")]
    internal class QuestCardSO : BaseCardSO
    {
        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<QuestLogic>();
        }
    }
}
