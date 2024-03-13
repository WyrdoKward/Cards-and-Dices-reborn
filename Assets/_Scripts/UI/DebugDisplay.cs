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
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(CardToWatch.transform.position);
    }
}
