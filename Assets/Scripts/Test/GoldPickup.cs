using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{

    public int value;
    public int PointsToGive;

    public GameObject pickupEffect;
    public GameManager gameManager;
    public HealthManager healthManager;
    public PlayerController player;

    //public Collider col;

    public Transform target;
    public float followSpeed;
    public bool followingPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        
        target = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        player = FindObjectOfType<PlayerController>();
        healthManager = FindObjectOfType<HealthManager>();
        gameManager = FindObjectOfType<GameManager>();

        followingPlayer = false;
        gameObject.SetActive(true);
        //pickupEffect = GameObject.Find("Gold Pickup Effect");
    }

    // Update is called once per frame
    void Update()
    {
        

        //pickupEffect = GameObject.Find("/Gold Pickup Effect");

        if (followingPlayer)
        {
            transform.LookAt(target.position);
            transform.Translate(0f, 0f, followSpeed * player.moveSpeed * Time.deltaTime);
        }

        /*if (col.tag == "Player")
        {
            Debug.Log("now colliding with the player");
            CollectGold();

        }*/

    }

    public void CollectGold()
    {
        /*if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddGold(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);
            healthManager.HealPlayer(1);
            Destroy(gameObject);
        }*/

        //pickupEffect = GameObject.Find("Gold Pickup Effect")

        Instantiate(pickupEffect, transform.position, transform.rotation);
        Debug.Log("adding the gold...");
        gameManager.AddGold(value);
        healthManager.HealPlayer(value);
        gameManager.AddPoints(PointsToGive);
        Debug.Log("gold addition complete");
        Destroy(gameObject);

        //pickupEffect = null;

    }

    void OnTriggerEnter()
    {
        followingPlayer = true;
    }


    /*public void replaceGold(GameObject object1)
    {
        Instantiate(gameObject);
    }*/
}

