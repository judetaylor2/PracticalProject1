using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool timedSwitch;
    public float switchTime;
    public bool switchOn;
    public List<GameObject> switchObjects;

    void Start()
    {
        switchOn = false;
    }

    void Update()
    {
        if (switchOn)
        {

            foreach (GameObject theObject in switchObjects)
            {
                theObject.SetActive(switchOn);
            }
        }
        else
        {

            foreach (GameObject theObject in switchObjects)
            {
                theObject.SetActive(switchOn);
            }
        }

        if (timedSwitch == true && switchOn == true)
        {
            Invoke("SetToFalse", switchTime);
        }
    }



    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(switchOn)
            {
                switchOn = false;
            }
            else
            {
                switchOn = true;
            }
            


        }
    }

    public void SetToFalse()
    {
        switchOn = false;
        CancelInvoke();
    }

}
