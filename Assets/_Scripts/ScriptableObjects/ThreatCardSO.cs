using Assets._Scripts.Cards.Logic;
using Assets._Scripts.Utilities.Cache;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Threat", menuName = "Card/Threat")]
    internal class ThreatCardSO : BaseCardSO
    {
        [Tooltip("Le nom du script à charger depuis GameData/CardsBehaviour/Threats")]
        public string BehaviourScriptToLoad;
        public float ExecuteThreatDuration;

        public override void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            base.InitializedCardWithScriptableObject(cardBodyGO);

            //On vérifie que le behaviour qu'on essaye d'ajouter existe
            if (ComponentCache.Registrations.ContainsKey(BehaviourScriptToLoad))
            {
                cardBodyGO.AddComponent<ThreatLogic>();
                var behaviourToLoad = ComponentCache.Registrations[BehaviourScriptToLoad];
                cardBodyGO.AddComponent(behaviourToLoad);
            }
            else
            {
                Debug.LogWarning($"{BehaviourScriptToLoad} is not a valid ComponentIdentifierAttribute");
            }
        }
    }
}
