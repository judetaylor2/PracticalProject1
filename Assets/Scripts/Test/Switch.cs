using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //variables
    public bool timedSwitch;
    public float switchTime;
    public bool switchOn;
    public List<GameObject> switchObjects;

    public Renderer theRenderer;

    public Material colourRed;
    public Material colourGreen;

    //private bool timerCheck;

    void Start()
    {
        //set values
        theRenderer.material = colourRed;
    }

    void Update()
    {
        /*if (switchOn)
        {
            //controls whether the object is active when the switch is in and off 
            foreach (GameObject theObject in switchObjects)
            {
                theObject.SetActive(switchOn);
            }
        }
        else //if the switch is not on
        {

            foreach (GameObject theObject in switchObjects)
            {
                theObject.SetActive(switchOn); // uses switchOn's true or false value
            }
        }*/

        if (switchOn)
        {
            //switchOn = false;
            theRenderer.material = colourGreen;
        }
        else if (!switchOn)
        {
            //switchOn = true;
            theRenderer.material = colourRed;
        }

        if (switchOn)
        {
           
        }

        //controls whether the object is active when the switch is in and off 
        


        //if the switch is timed, then set switchOn to false after a certain amount of time
        if (timedSwitch == true && switchOn == true) 
        {
            Invoke("SetToFalse", switchTime);
        }
    }


    //if the player is in the trigger, then make the switch equal to the oppersite value

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {


            if (switchOn)
            {
                switchOn = false;
            }
            else if (!switchOn)
            {
                switchOn = true;
            }


            foreach (GameObject theObject in switchObjects)
            {
                if (switchOn)
                {
                    theObject.SetActive(!theObject.active);
                    

                }
                else if (!switchOn)
                {
                    theObject.SetActive(!theObject.active);
                    
                }
                /*else if (timerCheck)
                {
                    theObject.SetActive(!theObject.active);
                    timerCheck = false;
                }*/

            }

        }
    }

    //when called, the switch is false. CancelInvoke stops the invoke from repeating the other way around
    public void SetToFalse()
    {
        foreach (GameObject theObject in switchObjects)
        {

                theObject.SetActive(!theObject.active);

        }

        switchOn = false;
        CancelInvoke();
    }

}
