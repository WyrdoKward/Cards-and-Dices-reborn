using Assets._Scripts.Utilities.Cache;
using System;
using UnityEngine;

namespace Assets._Scripts.GameData.CardsBehaviour.Pnj
{
    [ComponentIdentifierAttribute(Name = "JunkArtisan")]
    internal class JunkArtisan : PnjSpecificBehaviour
    {
        public override Action GetSpecificCombination()
        {
            return base.GetSpecificCombination();
        }

        public override void Talk()
        {
            base.Talk();
            Debug.Log($"Hello, I'm Junk Artisan and you can sell me Scrap Metal !");
        }

        protected override void Trade()
        {
            base.Trade();
        }
    }
}
