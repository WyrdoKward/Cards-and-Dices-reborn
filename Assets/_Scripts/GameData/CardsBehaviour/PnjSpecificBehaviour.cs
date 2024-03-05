﻿using System;

namespace Assets._Scripts.GameData.CardsBehaviour
{
    internal abstract class PnjSpecificBehaviour : BaseCardBehaviour
    {
        public abstract Action GetSpecificCombination();
        public abstract void Talk();
        public abstract void Trade();
    }
}