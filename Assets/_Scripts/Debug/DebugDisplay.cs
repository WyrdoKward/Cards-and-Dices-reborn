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
        //cardPos.text = CardToWatch.transform.position.ToString();
        // les 2 suivants sont dans le emme référentiel ok
        var transformPos = Camera.main.WorldToScreenPoint(CardToWatch.transform.position);
        cardPos.text = transformPos.ToString();
        var mouse = InputHelper.GetCursorPositionInWorld((RectTransform)CardToWatch.transform);
        //mouse.z = Camera.main.transform.position.z;
        ////
        //mouse.z = Camera.main.transform.position.z;

        mousePos.text = Camera.main.ScreenToWorldPoint(mouse).ToString();
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(CardToWatch.transform.position);
    }
}
