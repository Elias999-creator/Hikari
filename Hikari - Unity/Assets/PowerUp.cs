using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int add = 1;

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
        stats.currentHealth += add;

        Destroy(gameObject);
    }
}
