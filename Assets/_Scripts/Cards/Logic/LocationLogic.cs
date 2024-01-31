using UnityEngine;

namespace Assets._Scripts.Cards.Logic
{
    internal class LocationLogic : CardLogic
    {
        internal override bool HasReceipe()
        {
            var res = base.HasReceipe();
            return res;
        }

        public void Explore()
        {
            Debug.Log($"Exploring {gameObject}...");
        }


        internal override void FireActionForEndTimer()
        {
            base.FireActionForEndTimer();
            Explore();
        }
    }
}
