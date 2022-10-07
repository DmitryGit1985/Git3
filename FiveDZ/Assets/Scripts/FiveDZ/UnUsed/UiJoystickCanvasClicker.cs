using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UiJoystickCanvasClicker : MonoBehaviour
{
    private GraphicRaycaster uiRaycaster;
    private PointerEventData clickData;
    private List<RaycastResult> clickResults;
    private GameObject UiElementClicked;
    [SerializeField]
    private Image ButtonLeft;
    [SerializeField]
    private Image ButtonRight;
    [SerializeField]
    private Image ButtonUp;
    [SerializeField]
    private Image ButtonDown;
    void Start()
    {
        uiRaycaster = GetComponent<GraphicRaycaster>();
        clickData = new PointerEventData(EventSystem.current);
        clickResults = new List<RaycastResult>();
    }
    void Update()
    {
        GetUiElementsClicked();
    }
    private void GetUiElementsClicked()
    {
        clickData.position = Input.mousePosition;
        clickResults.Clear();
        uiRaycaster.Raycast(clickData, clickResults);
        foreach (RaycastResult result in clickResults)
        {
            UiElementClicked = result.gameObject;
        }
        //Debug.Log(UiElementClicked);
    }
}
