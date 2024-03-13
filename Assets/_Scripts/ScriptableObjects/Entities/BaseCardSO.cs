using Assets._Scripts.Utilities;
using Assets._Scripts.Utilities.Cache;
using System.Collections.Generic;
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
        [Tooltip("Si cette carte peut se transformer (via son BehaviourScriptToLoad), mettre le SO de ses autres formes ici")]
        public List<BaseCardSO> OtherForms = new List<BaseCardSO>();

        public float TimerDuration = GlobalVariables.DefaultTimerDuration; //TODO a déplacer dans une classe intertmédiaire => BaseTimedCardSO : BaseCardSO et en faire hériter les P, T, L... mais pas les R par exemple
        [Tooltip("Le nom du script à charger depuis GameData/CardsBehaviour/xxx")]
        public string BehaviourScriptToLoad;


        public void OnEnable()
        {
            //Vérifications d'intégrité des SO
            if (OtherForms.Count > 0 && string.IsNullOrEmpty(BehaviourScriptToLoad))
            {
                Debug.LogWarning($"{Name} référence {OtherForms.Count} transformation(s) mais n'a pas de 'BehaviourScriptToLoad' pour l('|es) utiliser.");
            }
        }

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
