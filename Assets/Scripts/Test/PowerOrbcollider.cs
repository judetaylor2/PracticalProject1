using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbcollider : MonoBehaviour
{

    public PowerOrbPickup orb;
    public AudioSource soundCollectOrb;
    // Start is called before the first frame update
    void Start()
    {
        soundCollectOrb = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("now colliding with the player");
            orb.CollectOrb();
            PlaySound();
            

        }
    }

    void PlaySound()
    {
        soundCollectOrb.Play();
    }
    
 
}
