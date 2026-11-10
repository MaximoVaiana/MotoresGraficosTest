using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.ProBuilder.Shapes;

public class BossMusic : MonoBehaviour
{
    public AudioSource bossSound;
    public ReciveDamage reciveDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossSound = GameObject.Find("BossSong").GetComponent<AudioSource>();
        reciveDamage = FindFirstObjectByType<ReciveDamage>();
        StartCoroutine(BossTheme());
    }

    IEnumerator BossTheme()
    {
        yield return new WaitForSeconds(6);
        bossSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (reciveDamage.health <= 0)
        {
            bossSound.Stop();
        }
    }
}
