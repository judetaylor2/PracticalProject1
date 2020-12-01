using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool switchOn;
    public List<GameObject> switchObjects;

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

                foreach (GameObject theObject in switchObjects)
                {
                    theObject.SetActive(switchOn);
                }
            }
            else
            {
                switchOn = true;

                foreach (GameObject theObject in switchObjects)
                {
                    theObject.SetActive(switchOn);
                }
            }
  
        }
    }

}
