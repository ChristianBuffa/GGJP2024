using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletInfo bulletInfo;

    public Rigidbody2D rb;

    public float FireCooldown;
    public float bulletSpeed;
    private int damage;
    public int bulletNumber;

    public void SetBulletValues(BulletInfo info)
    {
        if(info != null)
        {
            FireCooldown = info.FireCooldown;
            bulletSpeed = info.bulletSpeed;
            damage = info.damage;
            bulletNumber = info.bulletNumber;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SetBulletValues(bulletInfo);
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
