using UnityEngine;
using System.Collections;

public class BossSpawn : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Vector3 spawnPosition;
    public float tiempoJefe;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPosition = transform.position;
        StartCoroutine(Jefe());
    }

    IEnumerator Jefe()
    {
        yield return new WaitForSeconds(tiempoJefe);

        Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
