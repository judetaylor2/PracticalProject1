﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //basic player movement
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    public float gravityScale;
    public bool jumping;

    //player momentum
    public float momentumIncrease;
    public float momentumDecrease;
    public float minMoveSpeed;
    public float maxMoveSpeed;

    //advanced player movement and animation
    private Vector3 moveDirection;
    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;

    //knockback
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    //unlockables
    public bool unlockable1;
    public bool stunSlash;
    public bool PowerStone;
    public bool speedGlove;
    public bool groundPound;


    //double jump
    public int doubleJump;
    public int secondJump;
    public float jumpForceGround;

    //stun slash
    public bool enemyStuned;
    public float stunTime;

    //wall collision
    public bool isTouchingWall;

    //damage
    Enemy1 enemy1;
    private float nextAttackTime = 0f;
    public float attackRate;
    public float maxAttackRate;
    public float minAttackRate;
    public int damage;
    public int damageAddition;
    public HealthManager healthManager;

    //power meter
    public HealthBar powerMeter;
    public float powerMeterDuration;
    public float maxPowerMeterDuration;
    public float minPowerMeterDuration;

    //Crate
    public Crate crate;

    //ground pound
    public float groundPoundForce;
    public bool isGroundPounding;

    //sound
    public AudioSource soundJump;
    public AudioSource soundDoubleJump;
    //public AudioSource soundRun;
    public AudioSource soundAttack1;
    public AudioSource soundAttack2;
    public AudioSource soundGroundPound;
    //public AudioSource sound;



    // Start is called before the first frame update (Will only happen once)
    void Start()
    {

        controller = GetComponent<CharacterController>();

        isGroundPounding = false;

        //sound
        //jumpSound = GetComponent<AudioSource>()

    }

    // Update is called once per frame (Repeats every frame)
    void Update()
    {



        // Debug.Log(moveDirection.y);

        //Debug.Log(isGroundPounding);

        //if the player is not being knocked back, then run the following
        if (knockBackCounter <= 0)
        {
            //move the player (X and Z)
            float yStore = moveDirection.y; //yStore is the moveDirection of the y axis
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            //if the player controller is touching the ground, then reset the double jump and the jumpforce
            if (controller.isGrounded)
            {
                jumpForce = jumpForceGround;
                doubleJump = 0;
                moveDirection.y = 0f;
                jumping = false;
            }


            if (unlockable1)
            {
                if (doubleJump < 2)
                {


                    if (Input.GetButtonDown("Jump"))
                    {

                        //if the jump count is below 1, then add the extra jump height to the next jump, make jumping true and play doublejump sound
                        if (doubleJump == 1)
                        {
                            jumping = true;

                            soundDoubleJump.Play();

                            jumpForce = jumpForce + secondJump;


                        }

                        //make the player jump, adds 1 to the jump counter and play the jump sound
                        moveDirection.y = jumpForce;
                        doubleJump++;

                        soundJump.Play();

                        //}
                    }
                    /*else if (doubleJump >= 2) //if doublejump is two (or more)
                    {
                        //reset move directions and doublejump counter
                        if (controller.isGrounded)
                        {
                            moveDirection.y = 0f;
                            doubleJump = 0;
                            jumping = false;

                        }
                    }*/

                }
                else //jumps once if the unlockable1 is not unlocked
                {
                    if (controller.isGrounded)
                    {
                        if (Input.GetButtonDown("Jump"))
                        {
                            moveDirection.y = jumpForce;
                            doubleJump = (doubleJump++);
                            soundJump.Play();
                        }
                    }
                }

            }
            else //count down the knockback by the time
            {
                knockBackCounter -= Time.deltaTime;
            }

            //allows the player to move properly
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);

            //if any input is detected by unit's "Horizontal" and "Vertical" axis
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                //if the player is not in a wall trigger, then build up momentum as normal
                if (!isTouchingWall)
                {
                    moveSpeed = moveSpeed + momentumIncrease;
                }

                //soundRun.Play();

                //move the player in different directions based on camera look direction
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

            }
            //decrease momentum when the player is idle
            else if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
            {
                moveSpeed = moveSpeed - (momentumIncrease * momentumDecrease);
            }

            //stops the movespeed being below the minimum movespeed
            if (moveSpeed <= minMoveSpeed)
            {
                moveSpeed = minMoveSpeed;
            }

            //stops the movespeed being below the maximum movespeed
            if (moveSpeed >= maxMoveSpeed)
            {
                moveSpeed = maxMoveSpeed;
            }

            //allows the powermeter to be used when full
            if (powerMeter.slider.value == powerMeter.slider.maxValue)
            {
                PowerMeterComplete();
            }
            //-----------------------------------------------------------------------------------
            if (PowerStone)
            {
                powerMeterDuration = maxPowerMeterDuration;
            }
            else
            {
                powerMeterDuration = minPowerMeterDuration;
            }

            if (speedGlove)
            {
                attackRate = maxAttackRate;
            }
            else
            {
                attackRate = minAttackRate;
            }

            if (groundPound && !controller.isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveDirection.y = groundPoundForce;
                isGroundPounding = true;
                soundGroundPound.Play();
            }
            else if (controller.isGrounded)
            {
                moveDirection.y = 0f;

                isGroundPounding = false;


            }

            //Animate the player
            anim.SetBool("isGrounded", controller.isGrounded);
            anim.SetFloat("speed", (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"))));

            if (jumping = false && !controller.isGrounded)
            {
                moveDirection.y = 0;
            }

        }
    }


    //player knockback
    public void KnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;

        moveSpeed = minMoveSpeed;

    }

    public void PowerMeterComplete()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            damage = damage * 2;

            damageAddition = damageAddition * 2;

            healthManager.currentHealth = healthManager.currentHealth * 2;

            powerMeter.SetMinHealth(Convert.ToInt32(powerMeter.slider.minValue));

            Invoke("StopPowerMeter", powerMeterDuration);
        }


    }

    public void StopPowerMeter()
    {
        damage = damage / 2;

        damageAddition = damageAddition / 2;

        healthManager.currentHealth = healthManager.currentHealth / 2;

        //material change
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Unlockable1")
        {
            unlockable1 = true;
            Destroy(GameObject.Find("KeyModel1"));
        }

        if (collision.gameObject.name == "Unlockable2")
        {
            stunSlash = true;
            Destroy(GameObject.Find("KeyModel2"));
        }

        if (collision.gameObject.name == "Unlockable3")
        {
            PowerStone = true;
            Destroy(GameObject.Find("KeyModel3"));

        }

        if (collision.gameObject.name == "Unlockable4")
        {
            speedGlove = true;
            Destroy(GameObject.Find("KeyModel4"));

        }

        if (collision.gameObject.name == "Unlockable5")
        {
            groundPound = true;
            Destroy(GameObject.Find("KeyModel5"));

        }

        if (collision.gameObject.name == "Goal")
        {
            SceneManager.LoadScene("Level 1");

        }

    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isTouchingWall = true;
            moveSpeed = minMoveSpeed;

        }

        if (collision.gameObject.tag == "Crate")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                crate = collision.gameObject.GetComponent<Crate>();
                crate.BreakBox();
            }

        }

        if (collision.gameObject.tag == "Enemy1")
        {

            enemy1 = collision.gameObject.GetComponent<Enemy1>();

            if (stunSlash)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    soundAttack2.Play();
                    damage = 1;
                    enemyStuned = true;
                    enemy1.TakeDamage();
                    nextAttackTime = Time.time + stunTime / attackRate;
                    damage = 5;
                }
            }

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    soundAttack1.Play();
                    enemy1.TakeDamage();
                    nextAttackTime = Time.time + 1f / attackRate;
                }

            }




        }



        /*void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag == "BreakablePlatform" && groundPound && )
            {
                Destroy(GameObject.Find("BreakablePlatform"));

            }
        }*/

    }

    //when no longer in the wall trigger, then set isTouchingWall to false

    void OnTriggerExit(Collider collision)
    {


        if (collision.gameObject.tag == "Wall")
        {
            isTouchingWall = false;
        }



    }


    }

