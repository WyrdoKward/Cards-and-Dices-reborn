using Assets._Scripts.Utilities.Cache;
using System;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.StateMachines.Game
{
    internal class GameStartedState : GameBaseState
    {
        public override void Enter(IStateContext gameManager)
        {
            Debug.Log("Loading GameStartedState...");
            LoadComponentsCache();
        }

        public override void Exit(IStateContext gameManager)
        {
            //Debug.Log("Done loading GameStartedState");
        }

        public override void HandleInput(IStateContext gameManager)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState(IStateContext gameManager)
        {
            throw new System.NotImplementedException();
        }

        private void LoadComponentsCache()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var currentType in assembly.GetTypes().Where(_ => typeof(MonoBehaviour).IsAssignableFrom(_)))
                {
                    var attributes = currentType.GetCustomAttributes(typeof(ComponentIdentifierAttribute), false);
                    if (attributes.Length > 0)
                    {
                        var targetAttribute = attributes.First() as ComponentIdentifierAttribute;

                        if (ComponentCache.Registrations.ContainsKey(targetAttribute.Name))
                        {
                            Debug.LogWarning($"{targetAttribute.Name} already registered, it should not exists more than once.");
                            continue;
                        }

                        ComponentCache.Registrations.Add(targetAttribute.Name, currentType);
                        Debug.Log($"Registering {targetAttribute.Name}");
                    }
                }
            }
        }
    }
}
