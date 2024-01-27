using UnityEngine;

public class KamikazeUnicorn : BaseEnemy, IExplode
{
    [SerializeField] private float chargeRange;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float explosionRadius;
    [SerializeField] private int damage;


    private Vector3 chargeStartPosition;
    private bool charging = false;

    public override void Attack()
    {
        if (!charging)
        {
            charging = true;
            chargeStartPosition = transform.position;

            Vector3 chargePosition;

            if (playerTransform.position.x < transform.position.x)
            {
                chargePosition = transform.position + Vector3.left * chargeRange;
            }
            else
            {
                chargePosition = transform.position + Vector3.right * chargeRange;
            }

            StartCoroutine(ChargeTowardsPosition(chargePosition));
        }
    }
    
    private System.Collections.IEnumerator ChargeTowardsPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, chargeSpeed * Time.deltaTime);
            yield return null;
        }
        
        OnDeath();
        
        charging = false;

        TransitionToState(EnemyState.Stand);
    }

    public void Explode(Vector2 explosionPosition, float explosionRadius, int damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, this.explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            Player player = collider.GetComponent<Player>();
            
            if (player != null)
            {
                Debug.Log(damage);
                player.GetComponent<HealthComponent>().TakeDamage(damage);
            }
        }
    }

    public override void OnDeath()
    {
        Explode(transform.position, explosionRadius, damage);
        base.OnDeath();
    }
}
