using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private int playerMaxHealth = 1;
    
    private Player player;
    private BaseEnemy enemy;

    private void Awake()
    {
        player = GetComponent<Player>();
        enemy = GetComponent<BaseEnemy>();

        if (player != null)
            currentHealth = playerMaxHealth;
        if (enemy != null)
            currentHealth = maxHealth;
    }

    public void ShieldPickUp()
    {
        currentHealth++;
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
