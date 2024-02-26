using UnityEngine;

namespace Assets._Scripts.GameData.CardsBehaviour
{
    internal abstract class ThreatBehaviour : MonoBehaviour, ICardBehaviour
    {
        public abstract void ExecuteThreat();
    }
}
