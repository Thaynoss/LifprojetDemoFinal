using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLastChance : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;



    [SerializeField] private float maximumSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float JumpButtonGraceperiod;
    [SerializeField] private Transform cameraTransform;
    private Vector3 moveDirection;

    private Animator anim;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? JumpButtonPressedTime;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
      float moveZ = Input.GetAxis("Vertical");
      float moveX = Input.GetAxis("Horizontal");
      moveDirection = new Vector3(moveX,0,moveZ);
      float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);
      
      //fonctionne comme mon autre code pour le mouvement avec un blender three slider
      //anim.SetFloat("Input Magnitude",inputMagnitude,0.05f,Time.deltaTime);
      float speed = inputMagnitude* maximumSpeed;
      moveDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y,Vector3.up)* moveDirection;
      moveDirection.Normalize();
      ySpeed = ySpeed + Physics.gravity.y *Time.deltaTime;


            if (moveDirection !=Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.Mouse0)) //walk
            {
                //walkAnim
                speed = walkSpeed;
                moveSpeed = speed; //pour voir la vitesse (debug)
                anim.SetFloat("Input Magnitude",0.5f,0.1f,Time.deltaTime);
            }
            else if (moveDirection !=Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.Mouse0)) //Run
            {
                //RunAnim
                speed = runSpeed;
                moveSpeed = speed; //pour voir la vitesse (debug)

                anim.SetFloat("Input Magnitude",1f,0.1f,Time.deltaTime);
            }
            if(Input.GetMouseButtonDown(0) && moveDirection == Vector3.zero)
            {
                anim.SetTrigger("Attack");
            }

            if(Input.GetMouseButtonDown(1) && moveDirection == Vector3.zero)
            {
                anim.SetTrigger("PowerATK");
            }

            if(Input.GetKeyDown(KeyCode.T) && moveDirection == Vector3.zero)
            {
                anim.SetTrigger("Spell");
            }

      if (characterController.isGrounded)
      {
          lastGroundedTime = Time.time;
      }

      if(Input.GetKeyDown(KeyCode.Space))
      {
          //corriger jump infinit
          anim.SetTrigger("Jump");
          JumpButtonPressedTime = Time.time;
      }
        //pour faire des sauts plus rapidement sans bug et sans latence
      if(Time.time -lastGroundedTime <= JumpButtonGraceperiod)
      {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            
            if(Time.time - JumpButtonPressedTime <= JumpButtonGraceperiod)
            {
                ySpeed = jumpSpeed;
                JumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
      }
      else
      {
            characterController.stepOffset = 0;
      }

          if (moveDirection != Vector3.zero)
          {
              anim.SetBool("IsMoving", true);
              Quaternion toRotation = Quaternion.LookRotation(moveDirection,Vector3.up);
              transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation , rotationSpeed * Time.deltaTime);
              
          }
          else
          {
              anim.SetBool("IsMoving", false);
          }
         
        Vector3 velocity = moveDirection * speed;
        velocity.y = ySpeed ;

        characterController.Move(velocity*Time.deltaTime);


        moveDirection = moveDirection * speed;


    }

}
