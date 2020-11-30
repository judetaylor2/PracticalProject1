using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject goldObject;
    public GameObject powerOrbObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakBox()
    {
        if(goldObject)
        {
            goldObject.SetActive(true);
        }

        if (powerOrbObject)
        {
            powerOrbObject.SetActive(true);
        }

        Destroy(gameObject);
    }
}
