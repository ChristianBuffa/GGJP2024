using UnityEngine;

public class BaseExplodingEnemy : BaseEnemy, IExplode
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private int damage;

    public override void OnDeath()
    {
        Explode(transform.position, explosionRadius, damage);
        base.OnDeath();
    }

    public void Explode(Vector2 explosionPosition, float explosionRadius, int damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, this.explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            Player player = collider.GetComponent<Player>();
            
            if (player != null)
            {
                player.GetComponent<HealthComponent>().TakeDamage(damage);
            }
        }
    }
}
