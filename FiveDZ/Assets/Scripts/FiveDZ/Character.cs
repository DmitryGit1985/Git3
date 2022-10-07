using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private UiJoystick uiJoystickMove;
    [SerializeField] private UiJoystick uiJoystickRotate;
    [SerializeField] private ButtonClicked buttonJump;
    private CharacterController characterController;
    private Vector3 horizontalMovement;
    private Vector3 verticalMovement;
    [SerializeField] private float movementSpeed = 10.00f;
    private float gravity = -9.81f;
    [SerializeField] private float jumpSpeed = 10.00f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistanse = 0.5f;
    private float presision = 0.000f;
    private bool isJumping = false;
    private bool isGrounded;
    public CharacterController CharacterController
    {
        get => characterController = characterController ?? GetComponent<CharacterController>();
    }
    private void Start()
    {
        groundCheck.localPosition = new Vector3(0, -CharacterController.height / 2 + presision, 0);
        verticalMovement.y = gravity;
    }
    void Update()
    {
        Jumping(out verticalMovement.y);
        Movement();
        Rotation();
    }
    private void Movement()
    {
        //float vertical = Input.GetAxis("Vertical");
        //float horizontal = Input.GetAxis("Horizontal");
        float joysticHorizontalPush = uiJoystickMove.InputDirection.normalized.x;
        float joysticVerticalPush = uiJoystickMove.InputDirection.normalized.y;
        Vector3 direction = new Vector3(joysticHorizontalPush, 0, joysticVerticalPush);
        horizontalMovement = transform.TransformDirection(direction);// ак пон€ть в каких координатах сейчас??
        //CharacterController.Move(horizontalMovement * movementSpeed * Time.deltaTime);
        CharacterController.Move((verticalMovement +horizontalMovement * movementSpeed) * Time.deltaTime);
    }
    private void Rotation()
    {
        //float rotation = Input.GetAxis("Mouse X");
        float joysticHorizontalPush = uiJoystickRotate.InputDirection.normalized.x;
        CharacterController.transform.Rotate(Vector3.up * Time.deltaTime, joysticHorizontalPush);
    }
    private void Jumping(out float verticalMovementY)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistanse, groundMask);
        if (isGrounded && verticalMovement.y < 0)
        {
            isJumping = false;
            verticalMovement.y = 0;
        }
        //Input.GetButtonDown("Jump")
        if (buttonJump.ButtonPressed && isGrounded)
        {
            isJumping = true;
            verticalMovement.y += Mathf.Sqrt(jumpSpeed * -3.0f * gravity);
        }
        if (isJumping == true)
        {
            verticalMovement.y += gravity * Time.deltaTime;
        }
        else
        {
            verticalMovement.y = gravity;
        }
        verticalMovementY = verticalMovement.y;
        //CharacterController.Move(verticalMovement * Time.deltaTime);
    }
}
