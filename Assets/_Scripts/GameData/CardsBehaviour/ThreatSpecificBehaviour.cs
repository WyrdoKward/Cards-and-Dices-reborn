namespace Assets._Scripts.GameData.CardsBehaviour
{
    /// <summary>
    /// Specific behaviour for a particular threat card
    /// </summary>
    internal abstract class ThreatSpecificBehaviour : BaseCardBehaviour
    {
        public abstract void ExecuteThreat();
    }
}
