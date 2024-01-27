using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletInfo info;

    public Rigidbody2D rb;

    private float fireRate;
    public float bulletSpeed;
    private int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        fireRate = info.fireRate;
        bulletSpeed = info.bulletSpeed;
        damage = info.damage;
    }

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealthComponent>())
        {
            other.GetComponent<HealthComponent>().TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
