using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    public Renderer theRenderer;

    //public Material borderVisible;
    //public Material borderInvisible;

    void Start()
    {
        theRenderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            theRenderer.enabled = true;
        }
      
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            theRenderer.enabled = false;
        }
        
    }
}
