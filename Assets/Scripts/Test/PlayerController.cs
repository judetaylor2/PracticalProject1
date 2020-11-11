using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    public float gravityScale;
    
    public float momentumIncrease;
    public float momentumDecrease;
    public float minMoveSpeed;
    public float maxMoveSpeed;

    private Vector3 moveDirection;
    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;
    // Start is called before the first frame update (Will only happen once)
    void Start()
    {
       
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame (Repeats every frame)
    void Update()
    {
      

        
        if (knockBackCounter <= 0)
        {
            //move the player (X and Z)
            float yStore = moveDirection.y; //yStore is the moveDirection of the y axis
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            /*if(!Input.GetButtonDown("Vertical"))
            {
                moveSpeed = moveSpeed++;


            }*/

            //make the player jump (Y)
            if (controller.isGrounded)
            {
                moveDirection.y = 0f;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }


        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
        
        //move the player in different directions based on camera look direction
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            moveSpeed = moveSpeed + momentumIncrease;
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
        {
            moveSpeed = moveSpeed - (momentumIncrease * momentumDecrease);
        }

        if (moveSpeed <= minMoveSpeed)
        {
            moveSpeed = minMoveSpeed;
        }

        if (moveSpeed > maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
        }

        //Animate the player
        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("speed", (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")))); 

    }

    //player knockback
    public void KnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;

        moveSpeed = minMoveSpeed;

    }

    /*public void OnCollisionEnter(Collision stopMomentum)
    {
        if(stopMomentum.gameObject == "n")
        {
            moveSpeed = minMoveSpeed;
        }
    }*/

}

