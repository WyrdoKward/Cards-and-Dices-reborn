using UnityEngine;

namespace Assets._Scripts.Utilities
{
    internal static class InputHelper
    {
        /// <summary>
        /// Renvoie la position de la souris dans le monde ingame
        /// </summary>
        /// <param name="target">L'objet survolé lors d'un drag n drop par exemple</param>
        internal static Vector2 GetCursorPositionInWorld(RectTransform target)
        {
            //https://www.youtube.com/watch?v=uNCCS6DjebA
            Vector2 screenPosition = Input.mousePosition;
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            return worldPosition;
        }
    }
}
