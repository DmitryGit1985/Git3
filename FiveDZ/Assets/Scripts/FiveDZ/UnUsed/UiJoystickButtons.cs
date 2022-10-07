using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UiJoystickButtons : MonoBehaviour
{
    [SerializeField] private ButtonClicked joystickLeft;
    [SerializeField] private ButtonClicked joystickRight;
    [SerializeField] private ButtonClicked joystickUp;
    [SerializeField] private ButtonClicked joystickDown;

    private Vector3 inputDirection;
    public Vector3 InputDirection { get => inputDirection; }
    void Start()
    {
        inputDirection = Vector3.zero;
    }
    private void Update()
    {
        FindPressedButton();
    }
    private void FindPressedButton()
    {
        if(joystickLeft.ButtonPressed==true)
        {
            Debug.Log("joystickLeft");
        }
        if (joystickRight.ButtonPressed)
        {
            Debug.Log("joystickRight");
        }
        if (joystickUp.ButtonPressed)
        {
            Debug.Log("joystickUp");
        }
        if (joystickDown.ButtonPressed)
        {
            Debug.Log("joystickDown");
        }
    }
}
