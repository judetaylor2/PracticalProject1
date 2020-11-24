using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{

    public int value;

    public GameObject pickupEffect;
    public HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddGold(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);
            healthManager.HealPlayer(1);
            Destroy(gameObject);
        }
     
       
    }


    /*public void replaceGold(GameObject object1)
    {
        Instantiate(gameObject);
    }*/
}

