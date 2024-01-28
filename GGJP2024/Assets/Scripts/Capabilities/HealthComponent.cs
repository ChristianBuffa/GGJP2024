using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject scudo;
    private int currentHealth;

    private int playerMaxHealth = 1;
    public bool hasShield = false;
    
    private Player player;
    private BaseEnemy enemy;

    private void Awake()
    {
        player = GetComponent<Player>();
        enemy = GetComponent<BaseEnemy>();

        if (player != null)
        {
            currentHealth = playerMaxHealth;
            hasShield = true;
        }

        if (enemy != null)
            currentHealth = maxHealth;
    }

    public void ShieldPickUp()
    {
        if (!hasShield)
        {
            scudo.SetActive(true);
            hasShield = true;
            currentHealth++;
        }
    }

    public void TakeDamage(int damage)
    {
        if (hasShield)
        {
            scudo.SetActive(false);
            hasShield = false;
        }
        else
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
