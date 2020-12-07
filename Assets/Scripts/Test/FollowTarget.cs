using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float followSpeed;
    public bool followingPlayer;
    
    public float expireTime;

    public HurtPlayer hurtPlayer;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        //followingPlayer = false;

        hurtPlayer = GetComponent<HurtPlayer>();
        transform.LookAt(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        expireTime += Time.deltaTime;
        //target = GetComponent<PlayerController>().GetComponent<Transform>();
        

        transform.Translate(0f, 0f, followSpeed * Time.deltaTime);

        if (expireTime >= 10)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Collided with player");
        //Destroy(gameObject);

        
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }


    }

}
