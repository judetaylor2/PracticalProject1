using System;
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


            if (controller.isGrounded)
            {
                jumpForce = jumpForceGround;
                doubleJump = 0;
            }

            if (unlockable1)
            {
                if (doubleJump < 2)
                {


                    if (Input.GetButtonDown("Jump"))
                    {
                        if (doubleJump == 1)
                        {
                            jumpForce = jumpForce + secondJump;
                        }



                        moveDirection.y = jumpForce;
                        doubleJump = (doubleJump + 1);
                    }
                }
                else if (doubleJump >= 2)
                {
                    if (controller.isGrounded)
                    {
                        moveDirection.y = 0f;
                        doubleJump = 0;
                    }
                }
            }
            else
            {
                if (controller.isGrounded)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        moveDirection.y = jumpForce;
                        doubleJump = (doubleJump++);
                    }
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
            if (!isTouchingWall)
            {
                moveSpeed = moveSpeed + momentumIncrease;
            }


            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }
        //player is idle
        else if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0)
        {
            moveSpeed = moveSpeed - (momentumIncrease * momentumDecrease);
        }

        //
        if (moveSpeed <= minMoveSpeed)
        {
            moveSpeed = minMoveSpeed;
        }

        if (moveSpeed > maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
        }

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
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Unlockable1")
        {
            unlockable1 = true;
        }

        if (collision.gameObject.name == "Unlockable2")
        {
            stunSlash = true;
        }

        if (collision.gameObject.name == "Unlockable3")
        {
            PowerStone = true;

        }

        if (collision.gameObject.name == "Unlockable4")
        {
            speedGlove = true;

        }

        if (collision.gameObject.name == "Goal")
        {
            SceneManager.LoadScene("Level 1");

        }

    }

    public void OnTriggerStay(Collider collision)
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
                    enemy1.TakeDamage();
                    nextAttackTime = Time.time + 1f / attackRate;
                }

            }




        }

    }

    private void OnTriggerExit(Collider collision)
    {
     

        if (collision.gameObject.tag == "Wall")
        {
            isTouchingWall = false;
        }
   


    }   

    private void OnCollisionExit(Collision col)
    {
        moveDirection.y = -gravityScale;
    }
}       