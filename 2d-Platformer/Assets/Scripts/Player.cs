using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float inputDirection;
    private float verticalVelocity;
    private float gravity = 30f;
    private float speed = 5;
    private float jumpForce = 10f;

    private CharacterController characterController;
    private Vector3 moveVector;
    private Vector3 lastMotion;

    bool secondJumpAvailable = false;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = Vector3.zero;
        inputDirection = Input.GetAxis("Horizontal") * speed;

        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // make the player jump 
                verticalVelocity = jumpForce;
                secondJumpAvailable = true;
            }

            moveVector.x = inputDirection;
        } 
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (secondJumpAvailable)
                {
                    // make the player second jump 
                    verticalVelocity = jumpForce;
                    secondJumpAvailable = false;
                }
              
            }
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector.x = lastMotion.x;
        }

       
        moveVector.y = verticalVelocity;
       
        characterController.Move(moveVector * Time.deltaTime);
        lastMotion = moveVector;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(characterController.collisionFlags == CollisionFlags.Sides)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.red, 2f);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                moveVector = hit.normal * speed;
                verticalVelocity = jumpForce;
                secondJumpAvailable = true;
            }
        }
    }
}
