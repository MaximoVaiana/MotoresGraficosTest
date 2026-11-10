using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class EnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public Animator enemyAnimator;
    public Animator doorL;
    public Animator doorR;

    public bool isDead = false;
    public GameObject enemyHealth;
    public bool isBoss = false;
    public GameObject enemy;
    public AudioSource doorOpening;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorOpening = GameObject.Find("AudioDoor").GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("UpdateTarget",2f, 0.2f);
        enemyAnimator = GetComponentInChildren<Animator>();
        doorL = GameObject.Find("Door1").GetComponent<Animator>();
        doorR = GameObject.Find("Door2").GetComponent<Animator>();
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
            if (!isBoss)
            {
                StartCoroutine(EnemyDespawn());
            }

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

                StartCoroutine(DoorOpen());
            }
        }
    }

    IEnumerator DoorOpen()
    {
        yield return new WaitForSeconds(5);
        doorL.SetTrigger("MoveDoorL");
        doorR.SetTrigger("MoveDoorR");
        doorOpening.Play();
        doorOpening.Play();
    }
    IEnumerator EnemyDespawn()
    {
        yield return new WaitForSeconds(7);
        Destroy(enemy);
    }
}
