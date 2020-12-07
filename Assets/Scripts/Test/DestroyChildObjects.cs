using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildObjects : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(GameObject.Find("KeyModel"));
        }
    }

}
