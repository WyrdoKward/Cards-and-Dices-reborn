using System;

namespace Assets._Scripts.Utilities.Cache
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ComponentIdentifierAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
