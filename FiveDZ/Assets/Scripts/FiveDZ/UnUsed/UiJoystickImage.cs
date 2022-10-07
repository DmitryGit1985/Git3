using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiJoystickImage : MonoBehaviour
{
    [SerializeField] private Image ButtonLeft;
    [SerializeField] private Image ButtonRight;
    [SerializeField] private Image ButtonUp;
    [SerializeField] private Image ButtonDown;
    [SerializeField] private ImageEnter ButtonLeftEnter;
    [SerializeField] private ImageEnter ButtonRightEnter;
    [SerializeField] private ImageEnter ButtonUpEnter;
    [SerializeField] private ImageEnter ButtonDownEnter;
    private Vector3 inputDirection;
    public Vector3 InputDirection { get => inputDirection; }
    void Update()
    {
        ImageEntered();
    }
    private void ImageEntered()
    {
        if(ButtonLeftEnter.ImageName== ButtonLeft.name && ButtonLeftEnter.ImageEntered==true)
        {
            inputDirection = Vector3.left;
        }
        if (ButtonRightEnter.ImageName == ButtonRight.name && ButtonRightEnter.ImageEntered == true)
        {
            inputDirection = Vector3.right;
        }
        if (ButtonUpEnter.ImageName == ButtonUp.name && ButtonUpEnter.ImageEntered == true)
        {
            inputDirection = Vector3.up;
        }
        if (ButtonDownEnter.ImageName == ButtonDown.name && ButtonDownEnter.ImageEntered == true)
        {
            inputDirection = Vector3.down;
        }
        if (ButtonLeftEnter.ImageEntered == false && ButtonRightEnter.ImageEntered == false
            && ButtonUpEnter.ImageEntered == false && ButtonDownEnter.ImageEntered == false)
        {
            inputDirection = Vector3.zero;
        }
    }
}
