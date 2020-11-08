using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        //thePlayer = FindObjectOfType<PlayerController>();

        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if (invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                thePlayer.KnockBack(direction);

                invincibilityCounter = invincibilityLength;

                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }

        }

    }

    public void Respawn()
    {

        if(!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
        

    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnLength);
        isRespawning = false;

        thePlayer.gameObject.SetActive(true);

        GameObject player = GameObject.Find("Player");
        CharacterController charController = player.GetComponent<CharacterController>();
        charController.enabled = false;
        player.transform.position = respawnPoint;
        charController.enabled = true;

        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;


        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }    
    }
}
