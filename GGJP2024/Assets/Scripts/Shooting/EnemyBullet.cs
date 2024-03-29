using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private BulletInfo bulletInfo;

    public Rigidbody2D rb;
    private SpriteRenderer renderer;

    public float FireCooldown;
    public float bulletSpeed;
    private int damage;
    public int bulletNumber;
    public float bulletDeathTime;
    private Sprite sprite;

    public void SetBulletValues(BulletInfo info)
    {
        if(info != null)
        {
            FireCooldown = info.FireCooldown;
            bulletSpeed = info.bulletSpeed;
            damage = info.damage;
            bulletNumber = info.bulletNumber;
            bulletDeathTime = info.bulletDeathTime;
            sprite = info.sprite;

            renderer.sprite = sprite;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        SetBulletValues(bulletInfo);
    }

    private void Start()
    {
        Destroy(gameObject, bulletDeathTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (other.GetComponent<HealthComponent>())
            {
                other.GetComponent<HealthComponent>().TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }
    }
}