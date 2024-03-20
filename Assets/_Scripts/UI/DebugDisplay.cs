using Assets._Scripts.Utilities;
using TMPro;
using UnityEngine;

public class DebugDisplay : MonoBehaviour
{
    public GameObject CardToWatch;
    public TextMeshProUGUI cardPos;
    public TextMeshProUGUI mousePos;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // les 2 suivants sont dans le emme référentiel ok
        cardPos.text = CardToWatch.GetComponent<RectTransform>().position.ToString();
        var mouse = InputHelper.GetCursorPositionInWorld();
        mousePos.text = mouse.ToString();

        //FPS
        DisplayFps();
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(CardToWatch.transform.position);
    }

    #region FPS
    //FPS
    public TextMeshProUGUI fpsText;
    private float updateInterval = 0.25f; // Interval de mise à jour de l'affichage (en secondes)
    private float lastInterval;
    private int frames = 0;
    private float fps;
    private void DisplayFps()
    {
        ++frames;

        var timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;

            // Mettre à jour le texte avec le nombre de FPS arrondi
            fpsText.text = "FPS: " + Mathf.RoundToInt(fps);
        }
    }
    #endregion
}
