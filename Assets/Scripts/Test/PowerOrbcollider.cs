using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbcollider : MonoBehaviour
{

    //gameobjects
    public PowerOrbPickup orb;

    //sound
    public AudioSource soundCollectOrb;

    // Start is called before the first frame update
    void Start()
    {
        //assign variables and set values

        soundCollectOrb = GetComponent<AudioSource>();

        
    }

    // if the player is in the trigger, then run the Orbs' collectOrb method and play the sound for collecting orbs
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("orb now colliding with the player");
            orb.CollectOrb();
            PlaySound();
            

        }
    }

    //sound still only plays in the trigger, i need to fix this later
    void PlaySound()
    {
        soundCollectOrb.Play();
    }
    
 
}
