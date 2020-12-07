using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && player.isGroundPounding)
        {
            Destroy(gameObject);
        }
        

    }

}