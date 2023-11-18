using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Assets._Scripts.Utilities
{
    internal static class InputHelper
    {
        /// <summary>
        /// Renvoie la position de la souris dans le monde ingame
        /// </summary>
        /// <param name="target">L'objet survolé lors d'un drag n drop par exemple</param>
        internal static Vector3 GetCursorPositionInWorld(RectTransform target)
        {
            //https://www.youtube.com/watch?v=uNCCS6DjebA
            var screenPosition = Input.mousePosition;
            screenPosition.z = Camera.main.WorldToScreenPoint(target.position).z;

            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            return worldPosition;
        }
    }
}
