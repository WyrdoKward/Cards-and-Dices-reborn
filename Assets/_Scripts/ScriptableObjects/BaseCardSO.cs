using UnityEngine;

namespace Assets._Scripts.ScriptableObjects
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class BaseCardSO : ScriptableObject
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
    }
}
