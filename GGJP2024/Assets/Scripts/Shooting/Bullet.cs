using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletInfo info;

    public Rigidbody2D rb;

    private float fireRate;
    public float bulletSpeed;
    private float damage;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //deal damage
        }
        else
        {
            //explode
        }
        
        Destroy(gameObject);
    }
}
