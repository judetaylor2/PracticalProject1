﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive = 1;
    public bool knockBack;
    //public bool readyToDestroy;

    void start()
    {
       //readyToDestroy = false;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;

            if(knockBack)
            {
                FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);
                //readyToDestroy = true;
                //readyToDestroy = false;
            }
            else
            {
                FindObjectOfType<HealthManager>().HurtPlayerNoKnockBack(damageToGive, hitDirection);
               //readyToDestroy = true;
                //readyToDestroy = false;
            }
          
        }    
    }
}
