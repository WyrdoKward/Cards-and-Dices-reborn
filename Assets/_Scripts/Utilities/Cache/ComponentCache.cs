using System;
using System.Collections.Generic;

namespace Assets._Scripts.Utilities.Cache
{

    public static class ComponentCache
    {
        public static Dictionary<string, Type> Registrations = new Dictionary<string, Type>();
    }
}

