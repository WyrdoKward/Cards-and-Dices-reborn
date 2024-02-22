using Assets._Scripts.Systems.Timer;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    internal class TimeManager : MonoBehaviour
    {
        public GameObject timerSliderPrefab;

        public void DisplayTimerSlider(float duration, GameObject targetCard, Color sliderColor)
        {
            ////Calcul de la position du slider
            //var timerPosition = targetCard.transform.position;
            //timerPosition.y += 20;

            var timerPosition = new Vector3(0, targetCard.transform.position.y + 20, 0);

            var spawnedTimer = Instantiate(timerSliderPrefab, timerPosition, Quaternion.identity);
            spawnedTimer.transform.SetParent(targetCard.transform);

            Debug.Log($"Spawning {spawnedTimer} on {targetCard}");

            spawnedTimer.GetComponent<TimerSliderBehaviour>().maxTime = duration;
            spawnedTimer.GetComponent<TimerSliderBehaviour>().timerSlider.value = duration;
            //spawedTimer.GetComponent<TimerSliderBehaviour>().timerSlider.transform.Find("Fill Area/Fill").GetComponent<Image>().color = sliderColor;


            spawnedTimer.transform.localScale = new Vector3(1, 1, 1);
            spawnedTimer.GetComponent<RectTransform>().anchoredPosition = timerPosition;
            spawnedTimer.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            spawnedTimer.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            spawnedTimer.GetComponent<RectTransform>().pivot = new Vector2(0, 1);

            //todo : fill area/fill trop grand, voir anchorMax.X
            var fill = spawnedTimer.GetComponent<TimerSliderBehaviour>().timerSlider.transform.Find("Fill Area/Fill");
            fill.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            fill.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            fill.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        }

        /// <summary>
        /// Détruit l'objet TimerSlider associé à gameObject
        /// </summary>
        /// <param name="gameObject"></param>
        public void DestroyTimerSlider(GameObject gameObject)
        {
            var timerToDestroy = gameObject.GetComponentInChildren<TimerSliderBehaviour>();
            timerToDestroy?.DestroySelf();
        }
    }
}
