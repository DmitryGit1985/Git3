using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float lookSensitivity = 100.0F;
    [SerializeField] private float yAxisClampDegrees = 10.0F;
    [SerializeField] private UiJoystick uiJoystickLook;
    private float yRotation;

    void Update()
    {
        float joysticVerticalPush = uiJoystickLook.InputDirection.normalized.y;
        yRotation -= joysticVerticalPush;
        yRotation = Mathf.Clamp(yRotation, -yAxisClampDegrees, yAxisClampDegrees);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(yRotation, 0, 0), lookSensitivity * Time.deltaTime);
    }
}
//transform.localRotation = Quaternion.Euler(yRotation, 0, 0);