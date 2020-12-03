using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using UnityEngine;

public class PowerOrbPickup : MonoBehaviour
{

    public int value;
    public int PointsToGive;

    public GameObject pickupEffect;
    public HealthBar powerMeter;
    public PlayerController player;

    //public Collider col;

    public Transform target;
    public float followSpeed;
    public bool followingPlayer;
    public float randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        powerMeter = GameObject.FindGameObjectsWithTag("PowerMeter")[0].GetComponent<HealthBar>();
        player = FindObjectOfType<PlayerController>();
        followingPlayer = false;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        randomNumber = Random.Range(1, 10);

        if (followingPlayer)
        {
            transform.LookAt(target.position);
            transform.Translate(0f, 0f, followSpeed * player.moveSpeed * Time.deltaTime);
        }

    }

    public void CollectOrb()
    {

        Debug.Log("adding the Power Orbs...");
        Instantiate(pickupEffect, transform.position, transform.rotation);
        powerMeter.AddHealth(value);
        Debug.Log("power orb addition complete");
        Destroy(gameObject);


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
