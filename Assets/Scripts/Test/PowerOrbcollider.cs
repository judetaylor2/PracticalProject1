using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbcollider : MonoBehaviour
{

    //gameobjects
    public int value;
    public int PointsToGive;

    //public PowerOrbPickup orb;


    public HealthBar powerMeter;
    public PlayerController player;

    //sound
    public AudioSource soundCollectOrb;

    public bool collect;
    private float collectionTimer = 0;
    public GameObject orbObject;
    public int collectCount;


    public GameObject pickupEffect;
    public GameManager gameManager;
    public HealthManager healthManager;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //assign variables and set values
        target = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        powerMeter = GameObject.FindGameObjectsWithTag("PowerMeter")[0].GetComponent<HealthBar>();
        player = FindObjectOfType<PlayerController>();

        soundCollectOrb = GetComponent<AudioSource>();

        
    }

    void Update()
    {
        collectionTimer = collectionTimer + Time.deltaTime;


        if (collect)
        {
            if (collectCount < 1)
            {
                orbObject.SetActive(false);
                soundCollectOrb.Play();
                collectCount = collectCount + 1;

                Instantiate(pickupEffect, transform.position, transform.rotation);

            }

            if (collectionTimer >= 0.5)
            {
                

                Debug.Log("adding the Power Orbs...");
                powerMeter.AddHealth(value);
                Debug.Log("power orb addition complete");
                Destroy(gameObject);

                collectionTimer = 0;
            }
        }
    }
       


    // if the player is in the trigger, then run the Orbs' collectOrb method and play the sound for collecting orbs
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("orb now colliding with the player");

            collect = true;
            
        }
    }

    //sound still only plays in the trigger, i need to fix this later

    
 
}
