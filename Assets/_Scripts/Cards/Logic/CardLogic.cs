using Assets._Scripts.Cards.Common;
using UnityEngine;

namespace Assets._Scripts.Cards.Logic
{
    internal abstract class CardLogic : MonoBehaviour
    {
        internal virtual bool HasReceipe()
        {
            if (GetComponent<CardController>().PreviousCardInStack != null)
                throw new System.Exception("Do not call this on a card not first in its stack");

            // Si il n'y a plus de carte suivante
            if (GetComponent<CardController>().NextCardInStack == null)
            {
                return false;
            }

            return true;
        }

        internal virtual void FireActionForEndTimer()
        {
        }
    }
}
