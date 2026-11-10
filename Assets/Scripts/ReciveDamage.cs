using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReciveDamage : MonoBehaviour
{
    public int health = 50;
    public int maxHealth = 50;
    public TextMeshProUGUI healthText;
    public Image healthBar;
    public EnemyNavigation enemyNavigation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyNavigation = GetComponent<EnemyNavigation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            enemyNavigation.Die();
        }

        healthText.text = health.ToString();
        healthBar.fillAmount = (float)health / maxHealth;
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
