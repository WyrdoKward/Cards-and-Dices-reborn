using System;
using UnityEngine;

namespace Assets._Scripts.Systems.Timer
{
    /// <summary>
    /// Un timer avec slider intégré associé à une action à exécuter à la fin du compte à rebours
    /// </summary>
    internal class CardTimer
    {
        /// <summary>
        /// False pour les threats etc, true pour les cartes qui doivent conserver leurs ingrédients toute la durée
        /// </summary>
        public bool StopIfIngredientsRemoved;

        private Action action;
        private float remainingTime;


        public CardTimer(Action action, float delay, bool stopIfIngredientsRemoved = true)
        {
            this.action = action;
            this.remainingTime = delay;
            StopIfIngredientsRemoved = stopIfIngredientsRemoved;
        }

        /// <summary>
        /// Checks if timer is complete
        /// </summary>
        /// <returns>True if remainingTime is < 0</returns>
        internal bool Update()
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
            {
                action();
                return true;
            }
            return false;
        }
    }
}
