using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlatform : MonoBehaviour
{
    //variables
    public GameObject player;

    void start()
    {
        
    }

    //if the player is in the trigger, then they will become a child of the platform meaning that it will move with it
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = transform;

        }
    }

    //when the player exits the trigger, they no longer become a child of the platform 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = null;

        }
    }
}
