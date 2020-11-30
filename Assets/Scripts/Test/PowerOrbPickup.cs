using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine;

public class PowerOrbPickup : MonoBehaviour
{

    public int value;

    public GameObject pickupEffect;
    public HealthBar powerMeter;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            //Instantiate(pickupEffect, transform.position, transform.rotation);
            powerMeter.AddHealth(value);
            Destroy(gameObject);

        }


    }

}

