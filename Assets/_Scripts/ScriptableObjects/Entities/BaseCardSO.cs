using Assets._Scripts.Utilities.Cache;
using UnityEngine;

namespace Assets._Scripts.ScriptableObjects.Entities
{
    public abstract class BaseCardSO : ScriptableObject
    {
        public string Name;
        public string Description; // Short
        public string Lore; // Long description
        public Sprite Artwork;
        public bool IsUnique;
        [Tooltip("Le nom du script à charger depuis GameData/CardsBehaviour/xxx")]
        public string BehaviourScriptToLoad;

        public void Print()
        {
            Debug.Log($"{Name}{(IsUnique ? ", unique" : "")}");
        }

        public virtual void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            cardBodyGO.GetComponent<Canvas>().overrideSorting = true;
            cardBodyGO.GetComponent<Canvas>().sortingLayerName = "Table";

            if (string.IsNullOrEmpty(BehaviourScriptToLoad))
                return;

            //On vérifie que le behaviour qu'on essaye d'ajouter existe
            if (ComponentCache.Registrations.ContainsKey(BehaviourScriptToLoad))
            {
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
