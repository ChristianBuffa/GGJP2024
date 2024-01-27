using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private Player player;
    private BaseEnemy enemy;

    private void Awake()
    {
        currentHealth = maxHealth;

        player = GetComponent<Player>();
        enemy = GetComponent<BaseEnemy>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            if (player != null)
                player.OnDeath();
            else if (enemy != null)
                enemy.OnDeath();
        }
    }
}
