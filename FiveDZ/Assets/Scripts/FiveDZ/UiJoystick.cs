using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UiJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image joystickContainer;
    [SerializeField] private Image joystick;
    [SerializeField] private bool isFixed = false;
    private Vector3 inputDirection;

    private int boundSize = 3;
    public Vector3 InputDirection { get => inputDirection; }
    void Start()
    {
        inputDirection = Vector3.zero;
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        //To get InputDirection
        RectTransformUtility.ScreenPointToLocalPointInRectangle
                (joystickContainer.rectTransform,
                pointerEventData.position,
                pointerEventData.pressEventCamera,
                out Vector2 position);

        position.x = position.x / joystickContainer.rectTransform.sizeDelta.x;
        position.y = position.y / joystickContainer.rectTransform.sizeDelta.y;
        float x = (joystickContainer.rectTransform.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
        float y = (joystickContainer.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

        if (isFixed==true)
        {
            if (x < 0 && Mathf.Abs(x) > Mathf.Abs(y))
            {
                inputDirection = Vector3.left;
                y = 0;
            }
            if (x > 0 && Mathf.Abs(x) > Mathf.Abs(y))
            {
                inputDirection = Vector3.right;
                y = 0;
            }
            if (y > 0 && Mathf.Abs(y) > Mathf.Abs(x))
            {
                inputDirection = Vector3.up;
                x = 0;
            }
            if (y < 0 && Mathf.Abs(y) > Mathf.Abs(x))
            {
                inputDirection = Vector3.down;
                x = 0;
            }
        }
        else
        {
            inputDirection = new Vector3(x, y, 0);
        }
        Debug.Log(isFixed);
        inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;
        //to define the area in which joystick can move around
        joystick.rectTransform.anchoredPosition = 
            new Vector3(inputDirection.x * (joystickContainer.rectTransform.sizeDelta.x / boundSize), inputDirection.y * (joystickContainer.rectTransform.sizeDelta.y) / boundSize);
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        OnDrag(pointerEventData);
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        inputDirection = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }
}
