using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int health = 1;
    public int attack = 1;

    private HealthBar healthBar;

    private void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        PlayerCombat stats = player.GetComponent<PlayerCombat>();
        stats.currentHealth += health;
        stats.attackDamage += attack;

        healthBar.SetHealth(stats.currentHealth);

        Destroy(gameObject);
    }
}
