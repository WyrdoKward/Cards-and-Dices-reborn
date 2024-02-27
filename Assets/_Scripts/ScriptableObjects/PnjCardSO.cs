using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Pnj", menuName = "Card/Pnj")]
    internal class PnjCardSO : BaseCardSO
    {
        public float TalkDuration;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);
        }
    }
}
