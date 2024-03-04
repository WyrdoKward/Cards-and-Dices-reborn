using Assets._Scripts.Utilities.Cache;
using System;
using UnityEngine;

namespace Assets._Scripts.GameData.CardsBehaviour.Pnj
{
    [ComponentIdentifierAttribute(Name = "JunkArtisan")]
    internal class JunkArtisan : PnjSpecificBehaviour
    {
        public override Action GetSpecificReceipe()
        {
            throw new NotImplementedException();
        }

        public override void Talk()
        {
            Debug.Log($"Hello, I'm Junk Artisan !");
        }

        public override void Trade()
        {
            throw new NotImplementedException();
        }
    }
}
