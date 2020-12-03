using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollider : MonoBehaviour
{

    public GoldPickup gold;

    // Start is called before the first frame update
    void Start()
    {
        
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
            gold.CollectGold();
        }
    }
}
