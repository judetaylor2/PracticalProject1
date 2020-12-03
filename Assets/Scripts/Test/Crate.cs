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
        //Instantiate(goldObject, transform.position, transform.rotation);
    }

    public void BreakBox()
    {
        if(goldObject/* != null*/)
        {
            Instantiate(goldObject, transform.position, transform.rotation);
        }

        if (powerOrbObject/* != null*/)
        {
            Instantiate(powerOrbObject, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
