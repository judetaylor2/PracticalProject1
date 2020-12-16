using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Renderer theRenderer;

    public Material colourRed;
    public Material colourGreen;

    void Start()
    {
        theRenderer.material = colourRed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            theRenderer.material = colourGreen;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            theRenderer.material = colourRed;
        }

    }
}
