using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public HealthManager healthManager;

    public Renderer theRenderer;

    public Material checkPointOff;
    public Material checkPointOn;

    public GameObject checkPointEffect;

    public AudioSource soundCheckPoint;

    public int soundCount1;

    private bool showParticle;
    private float particleTimer;

    public bool checkPointIsTrue;
    // Start is called before the first frame update
    void Start()
    {
        //if this doesn't work, I can use healthmanager = findobjectoftype
        healthManager = FindObjectOfType<HealthManager>();
        particleTimer = 0;
        checkPointIsTrue = false;
    }

    // Update is called once per frame
    void Update()
    {
        particleTimer = particleTimer + Time.deltaTime;

        //Debug.Log(particleTimer);

        if (showParticle && particleTimer > 0.5)
        {
            Instantiate(checkPointEffect, transform.position, transform.rotation);
            particleTimer = 0;
         
        }



    }

    public void CheckPointTrue()
    {

        CheckPoint[] checkPoints = FindObjectsOfType<CheckPoint>();
        foreach (CheckPoint cp in checkPoints)

        {
            cp.CheckPointFalse();
        }

        theRenderer.material = checkPointOn;
        showParticle = true;
        checkPointIsTrue = true;
    }

    public void CheckPointFalse()
    {
        theRenderer.material = checkPointOff;
        showParticle = false;
        checkPointIsTrue = false;

    }

    private void OnTriggerEnter(Collider other)
    {



        if (other.tag.Equals("Player"))
        {
            healthManager.SetSpawnPoint(transform.position);

            if(!checkPointIsTrue)
            {
                soundCheckPoint.Play();
                checkPointIsTrue = true;
            }

            CheckPointTrue();
        }
    }
}
