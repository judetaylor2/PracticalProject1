using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public int pointsToGive;
    public float lookRadius = 10f;
    public int enemyHealth;
    public PlayerController player;
    public bool testBool;

    public bool waitFinished1;
    private bool attackLoop;

    public HealthBar enemyHealthBar;
    public HealthBar powerMeter;
    public GameManager gameManager;

    Transform target;
    NavMeshAgent agent;

    public int maxHealth;

    public int damageToGive = 1;

    private float nextAttackTime = 0f;
    public float attackRate;
    private float attackDelay = 0f;
    public float attackDelayTime;

    public GameObject bullet;
    public bool usingProjectiles;

    private float projectileTimer;

    public float speed;

    public GameObject coin;
    public GameObject powerOrb;
    private int orbCount;
    public int orbDropAmount;
    //public float projectileDelayTime;

    //public int minHealth;

    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerController>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyHealthBar.SetMaxHealth(maxHealth);
        enemyHealth = maxHealth;
        
        if (usingProjectiles)
        {
            agent.speed = speed;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        projectileTimer = projectileTimer + Time.deltaTime;

        if (distance < lookRadius)
        {

            FaceTarget();
            agent.SetDestination(target.position);


            if (usingProjectiles)
            {
                agent.speed = 0;
            }

            if (usingProjectiles && projectileTimer >= 1.25)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                projectileTimer = 0f;
            }

            

            

            /*if (distance <= agent.stoppingDistance)
            {
                //attack the player/target and face them
                FaceTarget();
            
            }*/
        }
        else
        {
            projectileTimer = 0f;

            if(usingProjectiles)
            {
                agent.speed = speed;
            }
     
        }
    }

    void FaceTarget()
    {

        transform.LookAt(target.position);
        /*Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.x));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);*/

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void TakeDamage()
    {
        enemyHealth = enemyHealth - player.damage;
        
        enemyHealthBar.SetHealth(enemyHealth);

        //powerMeter = player.damage + player.damageAddition;

        powerMeter.AddHealth(player.damage);

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(coin, transform.position, transform.rotation);
            
            while (orbCount < orbDropAmount)
            {
                Instantiate(powerOrb, transform.position, transform.rotation);
                orbCount++;
            }
            
        }


        //I still need to make sure that the enemyhealth subtracts the same amount of health per delay
        /*void OnTriggerStay(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {

                StartCoroutine(EnemyDamageCooldown());
                attackLoop = true;
            }

        }

        public IEnumerator EnemyDamageCooldown()
        {
            while (attackLoop)
            {
                attackLoop = false;

                yield return new WaitForSeconds(1f);
                yield return new WaitForSeconds(1f);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    enemyHealth = enemyHealth - damage;
                }

            }
        }  */
    }

    private void OnTriggerEnter(Collider other)
    {
        nextAttackTime = Time.time + 1f / attackRate;

        

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(player.enemyStuned)
            {
                attackDelayTime = 4f;

                if (Time.time >= attackDelay)
                {
                    player.enemyStuned = false;
                }
            }

            if (Time.time >= nextAttackTime && Time.time >= attackDelay)
            {
                nextAttackTime = Time.time + 1f / attackRate;

                attackDelay = Time.time + attackDelayTime;

                Vector3 hitDirection = other.transform.position - transform.position;
                hitDirection = hitDirection.normalized;

                FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);
                gameManager.AddPoints(pointsToGive);

                nextAttackTime = Time.time + 1f / attackRate;

                attackDelay = Time.time + attackDelayTime;

            }
        }
    }

}


