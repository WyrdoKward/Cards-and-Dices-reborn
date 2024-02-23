using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    public abstract class BaseCardSO : ScriptableObject
    {
        public string Name;
        public string Description; // Short
        public string Lore; // Long description
        public Sprite Artwork;
        public bool IsUnique;

        public void Print()
        {
            Debug.Log($"{Name}{(IsUnique ? ", unique" : "")}");
        }

        public virtual void InitializedCardWithScriptableObject(GameObject cardBodyGO)
        {
            cardBodyGO.GetComponent<Canvas>().overrideSorting = true;
        }
    }
}
