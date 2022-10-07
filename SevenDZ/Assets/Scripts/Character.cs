using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipMove1;
    [SerializeField] private AudioClip audioClipMove2;
    private bool audioClipMove1Paused=true;
    private bool audioClipMove2Paused=true;
    private float gravity = -9.81f;
    private Camera characterCamera;
    private CharacterController characterController;
    [SerializeField] private float mouseSensivity=0.5f;
    private Vector2 turn;
    float rotationAngle= 0.0f;
    public CharacterController CharacterController { get => characterController = characterController ?? GetComponent<CharacterController>(); }
    private void Start()
    {
        characterCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, gravity, vertical);
        turn.x += Input.GetAxis("Mouse X") * mouseSensivity;
        turn.y += Input.GetAxis("Mouse Y") * mouseSensivity;
        characterCamera.transform.localRotation= Quaternion.Euler(-turn.y, turn.x, 0.0f);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, characterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
        CharacterController.Move((rotatedMovement * movementSpeed) * Time.deltaTime);

        if (movement.sqrMagnitude > gravity*gravity)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClipMove1;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Pause();
        }

        /*
        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            //rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        }

        Quaternion currentRotation = CharacterController.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        CharacterController.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        */
    }
    IEnumerator playSoundInSeries()
    {
        audioSource.clip = audioClipMove1;
        audioSource.Play();
        yield return new WaitForSeconds(audioClipMove1.length);
        audioSource.clip = audioClipMove2;
        audioSource.Play();
    }
}