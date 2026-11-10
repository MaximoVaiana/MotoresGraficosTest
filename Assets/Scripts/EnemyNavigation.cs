using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Animator enemyAnimator;
    public bool isDead = false;
    public GameObject enemyHealth;
    public bool isBoss = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("UpdateTarget",2f, 0.2f);
        enemyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateTarget()
    {
        agent.destination = target.position;
    } 

    public void Die()
    {
        if (!isDead)
        {
            enemyAnimator.SetTrigger("Die");
            agent.isStopped = true;
            isDead = true;
            Destroy(enemyHealth);
            GetComponent<CapsuleCollider>().enabled = false;

            if (isBoss)
            {
                ReciveDamage[] enemies = FindObjectsByType<ReciveDamage>(FindObjectsSortMode.None);

                foreach (ReciveDamage enemy in enemies)
                {
                    if (enemy.gameObject != gameObject)
                    {
                        enemy.health = 0;
                    }
                }

                foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
                {
                    Destroy(spawner);
                }
            }
        }
    }
}
