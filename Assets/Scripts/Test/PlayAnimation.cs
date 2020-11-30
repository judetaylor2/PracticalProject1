using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{

    private Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        animation.SetBool("Laser_Spinning", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
