using Assets._Scripts.Cards.Logic;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects.Entities
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Follower", menuName = "Card/Follower")]
    internal class FollowerCardSO : BaseCardSO
    {

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            cardBodyGO.AddComponent<FollowerLogic>();
        }
    }
}
