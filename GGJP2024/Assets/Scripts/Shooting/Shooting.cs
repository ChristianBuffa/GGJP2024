using Unity.Mathematics;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public enum ShootingStance
    {
        Forward,
        Backwards,
        Up,
        Down,
        DiagonalFront,
        DiagonalBack
    }
    private float shootTimer;

    [SerializeField] private Transform spawnPosition;
    
    public Bullet currentBullet;

    private ShootingStance _shootingStance;
    private Move moveComponent;

    private void Awake()
    {
        moveComponent = GetComponent<Move>();
    }

    private void Update()
    {
        CheckInput();
        shootTimer -= Time.deltaTime;
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            _shootingStance = ShootingStance.DiagonalFront;
            moveComponent.enabled = false;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            _shootingStance = ShootingStance.DiagonalBack;
            moveComponent.enabled = false;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            _shootingStance = ShootingStance.Up;
            moveComponent.enabled = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _shootingStance = ShootingStance.Down;
            moveComponent.enabled = false;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _shootingStance = ShootingStance.Backwards;
            moveComponent.enabled = true;
        }
        else
        {
            _shootingStance = ShootingStance.Forward;
            moveComponent.enabled = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(shootTimer <= 0)
                Shoot();
        }
    }

    private void Shoot()
    {
        shootTimer = currentBullet.FireCooldown;
        
        if (_shootingStance == ShootingStance.Up)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,90);
            spawnPosition.position = transform.position + new Vector3(0, 1, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  Vector2.up * bullet.bulletSpeed;
        }
        else if (_shootingStance == ShootingStance.Down)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,-90);
            spawnPosition.position = transform.position + new Vector3(0, -1, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  Vector2.down * bullet.bulletSpeed;
        }
        else if(_shootingStance == ShootingStance.DiagonalFront)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,45);
            spawnPosition.position = transform.position + new Vector3(1, 1, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  new Vector2(0.5f, 0.5f) * bullet.bulletSpeed;
        }
        else if(_shootingStance == ShootingStance.DiagonalBack)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,135);
            spawnPosition.position = transform.position + new Vector3(-1, 1, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  new Vector2(-0.5f, 0.5f) * bullet.bulletSpeed;
        }
        else if (_shootingStance == ShootingStance.Backwards)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,180);
            spawnPosition.position = transform.position + new Vector3(-1, 0, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  Vector2.left * bullet.bulletSpeed;
        }
        else
        {
            Quaternion spawnRotation = quaternion.identity;
            spawnPosition.position = transform.position + new Vector3(1, 0, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  Vector2.right * bullet.bulletSpeed;
        }
    }
}
