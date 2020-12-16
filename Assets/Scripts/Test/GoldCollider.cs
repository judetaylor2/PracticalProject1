using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollider : MonoBehaviour
{
    public int value;
    public int PointsToGive;

    public HealthBar powerMeter;

    //public GoldPickup gold;

    public AudioSource soundCollectGold;
    public bool collect;
    private float collectionTimer = 0;
    public GameObject goldObject;
    private int collectCount;


    public GameObject pickupEffect;
    public GameManager gameManager;
    public HealthManager healthManager;
    public PlayerController player;

    public Transform target;

    //public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        player = FindObjectOfType<PlayerController>();
        healthManager = FindObjectOfType<HealthManager>();
        gameManager = FindObjectOfType<GameManager>();

        soundCollectGold = GetComponent<AudioSource>();
        //pickupEffect = GameObject.Find("/Gold Pickup Effect");
    }

    void Update()
    {
        collectionTimer = collectionTimer + Time.deltaTime;



        if (collect)
        {
            if (collectCount < 1)
            {
                goldObject.SetActive(false);
                soundCollectGold.Play();
                collectCount = collectCount + 1;

                Instantiate(pickupEffect, transform.position, transform.rotation);

            }

            if (collectionTimer >= 0.5)
            {
                
                Debug.Log("adding the gold...");
                gameManager.AddGold(value);
                healthManager.HealPlayer(value);
                gameManager.AddPoints(PointsToGive);
                Debug.Log("gold addition complete");
                Destroy(gameObject);

                collectionTimer = 0;
            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("gold now colliding with the player");
            
            collect = true;
            
        }

   
    }
}
