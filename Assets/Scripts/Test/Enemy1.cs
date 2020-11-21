using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{

    public float lookRadius = 10f;
    public int enemyHealth;
    public int damage;
    public bool testBool;

    public bool waitFinished1;
    private bool attackLoop;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
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


    //I still need to make sure that the enemyhealth subtracts the same amount of health per delay
    void OnTriggerStay(Collider collision)
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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                enemyHealth = enemyHealth - damage;
            }
          
        }
    }
}

