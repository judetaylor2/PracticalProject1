using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool switchOn;

    void Start()
    {
        switchOn = false;
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(switchOn)
            {
                switchOn = false;
            }
            else if (!switchOn)
            {
                switchOn = true;
            }
  
        }
    }

}
