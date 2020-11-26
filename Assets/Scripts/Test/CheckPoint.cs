using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public HealthManager healthManager;

    public Renderer theRenderer;

    public Material checkPointOff;
    public Material checkPointOn;

    // Start is called before the first frame update
    void Start()
    {
        //if this doesn't work, I can use healthmanager = findobjectoftype
        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPointTrue()
    {
        CheckPoint[] checkPoints = FindObjectsOfType<CheckPoint>();
        foreach (CheckPoint cp in checkPoints)
        {
            cp.CheckPointFalse();
        }

        theRenderer.material = checkPointOn;
    }

    public void CheckPointFalse()
    {
        theRenderer.material = checkPointOff;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            healthManager.SetSpawnPoint(transform.position);
            CheckPointTrue();
        }
    }
}
