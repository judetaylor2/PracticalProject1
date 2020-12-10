using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    public float speed;
    public Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponent<Renderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * speed;

        renderer.material.SetTextureOffset("_MainTex", new Vector2 (offset, 0));
    }
}
