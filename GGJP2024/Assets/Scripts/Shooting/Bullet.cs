using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletInfo info;

    public Rigidbody2D rb;

    public float FireCooldown;
    public float bulletSpeed;
    private float damage;

    public void SetBulletValues()
    {
        if(info != null)
        {
            FireCooldown = info.FireCooldown;
            bulletSpeed = info.bulletSpeed;
            damage = info.damage;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SetBulletValues();
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
