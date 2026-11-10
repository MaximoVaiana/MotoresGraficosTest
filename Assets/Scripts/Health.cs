using StarterAssets;
using System.Collections;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private Rigidbody rb;
    public int health = 5;
    public float damageCooldown;
    public bool damageCooldownDone = true;
    public TextMeshProUGUI healthPoints;
    public GameObject gameOver;
    public FirstPersonController firstPersonController;
    public AudioSource music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        music.Play();
        StartCoroutine(MusicStop());
    }

    IEnumerator MusicStop()
    {
        yield return new WaitForSeconds(100);
        music.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
            firstPersonController.enabled = false;
            gameOver.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
        }

        healthPoints.text = ("HP: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && damageCooldownDone)
        {
            health--;
            damageCooldownDone = false;
            StartCoroutine(CooldownDamage());
        }

        IEnumerator CooldownDamage()
        {
            yield return new WaitForSeconds(damageCooldown);
            damageCooldownDone = true;
        }
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
