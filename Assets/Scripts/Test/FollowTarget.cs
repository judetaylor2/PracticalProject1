using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float followSpeed;
    public bool followingPlayer;
    public float randomNumber;
  
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        followingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        randomNumber = Random.Range(1,10);

        if (followingPlayer)
        {
            transform.LookAt(target.position);
            transform.Translate(0f, 0f, followSpeed * randomNumber * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            followingPlayer = true;
        }
    }
}
