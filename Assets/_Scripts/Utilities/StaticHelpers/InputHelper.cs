using UnityEngine;

namespace Assets._Scripts.Utilities
{
    internal static class InputHelper
    {
        /// <summary>
        /// Renvoie la position de la souris dans le monde ingame
        /// </summary>
        internal static Vector2 GetCursorPositionInWorld()
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return worldPosition;
        }
    }
}
