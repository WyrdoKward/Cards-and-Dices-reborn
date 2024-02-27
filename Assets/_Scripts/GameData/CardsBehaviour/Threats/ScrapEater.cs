using Assets._Scripts.Utilities.Cache;
using UnityEngine;

namespace Assets._Scripts.GameData.CardsBehaviour.Threats
{

    [ComponentIdentifierAttribute(Name = "ScrapEater")]
    internal class ScrapEater : ThreatSpecificBehaviour
    {
        // TODO : AU lieu d'utiliser cette ref dans le SO, voir si à partir de la ref dans le SO on peut pas plutot référencer ce script dans le GO de base de la carte
        public override void ExecuteThreat()
        {
            Debug.Log("NOM NOM NOM scrap");
            var cardToEat = CardProvider.GetARandomCardByName("Scrap Metal");
            if (cardToEat != null)
            {
                GameObject.Destroy(cardToEat);
            }
            Destroy(gameObject);
        }
    }
}
