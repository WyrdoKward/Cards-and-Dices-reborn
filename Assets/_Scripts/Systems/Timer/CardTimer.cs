using Assets._Scripts.Managers;
using System;
using UnityEngine;

namespace Assets._Scripts.Systems.Timer
{
    /// <summary>
    /// Un timer avec slider intégré associé à une action à exécuter à la fin du compte à rebours
    /// </summary>
    internal class CardTimer
    {
        private readonly TimeManager TimeManager;
        /// <summary>
        /// False pour les threats etc, true pour les cartes qui doivent conserver leurs ingrédients toute la durée
        /// </summary>
        public bool StopIfIngredientsRemoved;

        private Action action;
        private float remainingTime;



        public CardTimer(GameObject card, Action action, float delay, bool stopIfIngredientsRemoved = true)
        {
            this.action = action;
            this.remainingTime = delay;
            StopIfIngredientsRemoved = stopIfIngredientsRemoved;

            TimeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
            TimeManager.DisplayTimerSlider(delay, card, Color.magenta);
        }

        /// <summary>
        /// Destoy the timer and the slider associated
        /// </summary>
        public void Destroy(GameObject gameObject)
        {
            TimeManager.DestroyTimerSlider(gameObject);
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
