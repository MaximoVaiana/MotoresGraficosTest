using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReciveDamage : MonoBehaviour
{
    public int health = 50;
    public TextMeshProUGUI healthText;
    public Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        healthText.text = health.ToString();
        healthBar.fillAmount = health / 100f *2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);

            health = health - 10;
        }
    }
}
