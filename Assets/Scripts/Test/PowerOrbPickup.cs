using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine;

public class PowerOrbPickup : MonoBehaviour
{
    //variables

    //points
    public int value;
    public int PointsToGive;

    //objects
    public GameObject pickupEffect;

    //scripts
    public HealthBar powerMeter;
    public PlayerController player;

    //public Collider col;

    //movement
    public Transform target;
    public float followSpeed;
    public bool followingPlayer;
    

    // Start is called before the first frame update
    void Start()
    {  
        //assign variables and set values
        target = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        powerMeter = GameObject.FindGameObjectsWithTag("PowerMeter")[0].GetComponent<HealthBar>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //follows the transform of the player

        if (followingPlayer)
        {
            transform.LookAt(target.position);
            transform.Translate(0f, 0f, followSpeed * player.moveSpeed * Time.deltaTime);
        }

    }


    //instantiate the orb pickup effect, add the orb value to the powermeter and destroy the gameobject
    public void CollectOrb()
    {

        Debug.Log("adding the Power Orbs...");
        Instantiate(pickupEffect, transform.position, transform.rotation);
        powerMeter.AddHealth(value);
        Debug.Log("power orb addition complete");
        Destroy(gameObject);


    }

    //follows the player when they enter the trigger
    void OnTriggerEnter()
    {
        followingPlayer = true;
    }


    /*public void replaceGold(GameObject object1)
    {
        Instantiate(gameObject);
    }*/
}



























/*using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine;

public class PowerOrbPickup : MonoBehaviour
{

    public int value;

    public GameObject pickupEffect;
    public HealthBar powerMeter;
    public PlayerController player;

    public Transform target;
    public float followSpeed;
    public bool followingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (followingPlayer)
        {
            transform.LookAt(target.position);
            transform.Translate(0f, 0f, followSpeed * player.moveSpeed * Time.deltaTime);
        }
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

*/
