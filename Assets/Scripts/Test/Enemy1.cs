using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{

    public float lookRadius = 10f;
    public int enemyHealth;
    public PlayerController player;
    public bool testBool;

    public bool waitFinished1;
    private bool attackLoop;

    public HealthBar enemyHealthBar;
    public HealthBar powerMeter;

    Transform target;
    NavMeshAgent agent;

    public int maxHealth;

    public int damageToGive = 1;

    private float nextAttackTime = 0f;
    public float attackRate;
    private float attackDelay = 0f;
    public float attackDelayTime;
    

    

    //public int minHealth;

    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerController>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyHealthBar.SetMaxHealth(maxHealth);
        enemyHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //attack the player/target and face them
                FaceTarget();

            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.x));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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

                nextAttackTime = Time.time + 1f / attackRate;

                attackDelay = Time.time + attackDelayTime;

            }
        }
    }

}


