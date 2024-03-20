using System;

namespace Assets._Scripts.GameData.CardsBehaviour.Actions
{
    /// <summary>
    /// Nom de l'action du CardBehaviour à afficher sur l'UI
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ActionNameAttribute : Attribute
    {
        public string Name { get; }

        public ActionNameAttribute(string name)
        {
            Name = name;
        }
    }

}
