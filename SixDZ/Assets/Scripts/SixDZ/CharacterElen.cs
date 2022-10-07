using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterElen : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float jumpSpeed = 7.0f;
    [SerializeField] private float animationBlendSpeed = 0.2f;
    [SerializeField] private AnimationClip animationClipEllenCombo1;
    [SerializeField] private AnimationClip animationClipEllenCombo2;
    [SerializeField] private AnimationClip animationClipEllenCombo3;
    [SerializeField] private AnimationClip animationClipEllenCombo4;
    private CharacterController characterController;
    private Animator characterAnimator;
    private Camera characterCamera;
    private float rotationAngle = 0.0f;
    private float targetAnimationSpeed = 0.0f;
    private float speedY = 0.0f;
    private float gravity = -9.81f;
    private bool isSprint = false;
    private bool isSpawn = false;
    private bool isDeath = false;
    private bool isJumping = false;
    private bool isAttacking = false;

    public CharacterController CharacterController { get => characterController = characterController ?? GetComponent<CharacterController>(); }
    public Animator CharacterAnimator { get => characterAnimator = characterAnimator ?? GetComponent<Animator>(); }
    public Camera CharacterCamera { get => characterCamera = characterCamera ?? FindObjectOfType<Camera>(); }
    private void Awake()
    {
        animationClipEllenCombo1.AddEvent(new AnimationEvent()
        {
            time = animationClipEllenCombo1.length,
            functionName = "OnCompletedAttackedAnimation"
        });
        animationClipEllenCombo2.AddEvent(new AnimationEvent()
        {
            time = animationClipEllenCombo1.length,
            functionName = "OnCompletedAttackedAnimation"
        });
        animationClipEllenCombo3.AddEvent(new AnimationEvent()
        {
            time = animationClipEllenCombo1.length,
            functionName = "OnCompletedAttackedAnimation"
        });
        animationClipEllenCombo4.AddEvent(new AnimationEvent()
        {
            time = animationClipEllenCombo1.length,
            functionName = "OnCompletedAttackedAnimation"
        });
    }
    private void Start()
    {
        CharacterAnimator.SetTrigger("Spawn");
        isSpawn = true;
        isAttacking = false;

        #region TestHashCode UnUsed
        int subStateAttackCombo1 = Animator.StringToHash("Base Layer.Attack.Combo1");
        int subStateAttackCombo2 = Animator.StringToHash("Base Layer.Attack.Combo2");
        int subStateAttackCombo3 = Animator.StringToHash("Base Layer.Attack.Combo3");
        int subStateAttackCombo4 = Animator.StringToHash("Base Layer.Attack.Combo4");
        int baseStateSpawn = Animator.StringToHash("Base Layer.Spawn");
        int baseStateDeath = Animator.StringToHash("Base Layer.Death");
        int subStateAttackComboSLength = 4;
        int[] subStateAttackComboS = new int[subStateAttackComboSLength];
        subStateAttackComboS.SetValue(subStateAttackCombo1,0);
        subStateAttackComboS.SetValue(subStateAttackCombo2, 1);
        subStateAttackComboS.SetValue(subStateAttackCombo3, 2);
        subStateAttackComboS.SetValue(subStateAttackCombo4, 3);
        #endregion
    }
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        isSprint = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKey(KeyCode.P))
        {
            if (isDeath != true)
            {
                CharacterAnimator.SetTrigger("Death");
            }
            isDeath = true;
        }
        
        if (Input.GetButtonDown("Fire1")&& isAttacking == false)
        {
            if (isDeath != true)
            {
                isAttacking = true;
                int startCombo = 1;
                int endCombo = 4;
                int randomCombo = UnityEngine.Random.Range(startCombo, endCombo);
                Debug.Log(randomCombo);//Как сделать чтобы выдало 1 цифру в Update?
                CharacterAnimator.SetTrigger("Attack");
                CharacterAnimator.SetInteger("Combo", randomCombo);

            }
        }
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            if (isDeath != true)
            {
                isJumping = true;
                CharacterAnimator.SetTrigger("Jumping");
                speedY += jumpSpeed;
                CharacterAnimator.SetBool("IsJumping", true);
                CharacterAnimator.SetBool("IsGrounded", false);
            }
        }
        if (CharacterController.isGrounded == false)
        {
            speedY += gravity * Time.deltaTime;
            CharacterAnimator.SetBool("IsGrounded", false);
        }
        else if (speedY < 0.0f)
        {
            speedY = 0.0f;
        }
        CharacterAnimator.SetFloat("SpeedY", speedY / jumpSpeed);

        if (isJumping == true && speedY < 0.0f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                CharacterAnimator.SetTrigger("Grounded");
                CharacterAnimator.SetBool("IsJumping", false);
                CharacterAnimator.SetBool("IsGrounded", true);
            }
        }
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
        Vector3 verticalMovement = Vector3.up * speedY;
        if (isSpawn == true)
        {
            if (AnimatorIsPlaying("Spawn") == true)
            {
                rotatedMovement = Vector3.zero;
            }
            else
            {
                isSpawn = false;
            }
        }
        if (isDeath == true)
        {
            rotatedMovement = Vector3.zero;
            verticalMovement = Vector3.zero;
            //isJumping = true;
        }
        float currentSpeed = isSprint ? sprintSpeed : movementSpeed;
        CharacterController.Move((verticalMovement + rotatedMovement * currentSpeed) * Time.deltaTime);

        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            targetAnimationSpeed = isSprint ? 1.0f : 0.5f;
        }
        else
        {
            targetAnimationSpeed = 0.0f;
        }
        CharacterAnimator.SetFloat("Speed", Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
        Quaternion currentRotation = CharacterController.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        CharacterController.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private bool AnimatorIsPlaying(string stateName)
    {
        return CharacterAnimator.GetCurrentAnimatorStateInfo(0).length > CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime
            && CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
    private void OnCompletedAttackedAnimation(string message)
    {
        isAttacking = false;
    }

    #region Test StartCoroutine("OnCompleteAttackAnimation"); not working
    IEnumerator OnCompleteAttackAnimation()
    {
            while (CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
            isAttacking = false;
        Debug.Log("AttackAnimationEnd");
    }
    IEnumerator OnCompleteAttackAnimation2()
    {
        while (CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return new WaitUntil(() => CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        isAttacking = false;
        Debug.Log("AttackAnimationEnd");
    }
    IEnumerator OnCompleteSpawnAnimation()
    {
        while (CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;
        isSpawn = true;
    }
    #endregion
}
/*
int baseState = Animator.StringToHash(Base Layer.Idle);
int short_baseState = Animator.StringToHash(Idle);
int subState = Animator.StringToHash(Base Layer.Jump_Fall_Roll.Roll);
int short_subState = Animator.StringToHash(Roll);

AnimatorStateInfo s = myAnimator.GetCurrentAnimatorStateInfo(0);
Debug.Log(s.fullPathHash + , +baseState);   //they are equal at Idle state in base layer
Debug.Log(s.shortNameHash + , +short_baseState);   //they are equal at Idle state in base layer
Debug.Log(s.fullPathHash + , +subState);   //they are equal at Roll state in sub-state machine
Debug.Log(s.shortNameHash + , +short_subState);   //they are equal at Roll state in sub-state machine
*/
/*Debug.Log(subStateAttackCombo1);
        Debug.Log(CharacterAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash);

        if (subStateAttackCombo1 == CharacterAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash)
        {
            Debug.Log("Attack");
        }*/


/*foreach (int subStateAttackCombo in subStateAttackComboS)
{
    if (subStateAttackCombo== CharacterAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash&& CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
    {
        Debug.Log("Attack");
        isAttacking = true;
    }
    else
    {
        isAttacking = false;
    }
}*/

/*if (CurrentAnimatorIsEnding() == true)
{
    //Debug.Log("Attack1");
    isAttacking = false;
}*/
/* if (CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack.Combo1") &&
         CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
 {
     Debug.Log("Base Layer.Attack.Combo1");
     //isAttacking = false;
     //CharacterAnimator.SetBool("Attack", false);
 }*/
