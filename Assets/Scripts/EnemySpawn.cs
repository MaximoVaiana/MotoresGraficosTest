using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Vector3 spawnPosition;
    public float delay;
    public float tiempoRepeticion;
    public float cambioSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosition = transform.position; 
        InvokeRepeating("CrearEnemigo", delay, tiempoRepeticion);
        StartCoroutine(DestruirSpawner());
    }

    IEnumerator DestruirSpawner()
    {   
        yield return new WaitForSeconds(cambioSpawner);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CrearEnemigo()
    {
        Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
    }
}
