using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollider : MonoBehaviour
{

    public GoldPickup gold;

    public AudioSource soundCollectGold;
    //public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        soundCollectGold = GetComponent<AudioSource>();
        //pickupEffect = GameObject.Find("/Gold Pickup Effect");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("gold now colliding with the player");
            gold.CollectGold();
            PlaySound();
        }

        void PlaySound()
        {
            soundCollectGold.Play();
        }
    }
}
