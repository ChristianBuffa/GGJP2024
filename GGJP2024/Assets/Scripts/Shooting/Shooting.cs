using System;
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
    
    private Animator animator;
    private float shootTimer;

    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float bulletSpawnRange = 2f;
    [SerializeField] private BulletInfo standardBulletInfo;
    
    public Bullet currentBullet;
    public static bool IsShooting = false;

    private Move moveComponent;

    private ShootingStance _shootingStance;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
            if (shootTimer <= 0)
                Shoot();
            else
                IsShooting = false;
        }
    }

    private void Shoot()
    {
        IsShooting = true;
        
        shootTimer = currentBullet.FireCooldown;
        
        if (_shootingStance == ShootingStance.Up)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,90);
            spawnPosition.position = transform.position + new Vector3(0, bulletSpawnRange, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  Vector2.up * bullet.bulletSpeed;
            animator.SetTrigger("ShootUp");
        }
        else if (_shootingStance == ShootingStance.Down)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,-90);
            spawnPosition.position = transform.position + new Vector3(0, -bulletSpawnRange, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  Vector2.down * bullet.bulletSpeed;
            animator.SetTrigger("ShootDown");
        }
        else if(_shootingStance == ShootingStance.DiagonalFront)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,45);
            spawnPosition.position = transform.position + new Vector3(bulletSpawnRange, bulletSpawnRange, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  new Vector2(0.5f, 0.5f) * bullet.bulletSpeed;
            animator.SetTrigger("ShootUpR");
        }
        else if(_shootingStance == ShootingStance.DiagonalBack)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,135);
            spawnPosition.position = transform.position + new Vector3(-bulletSpawnRange, bulletSpawnRange, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  new Vector2(-0.5f, 0.5f) * bullet.bulletSpeed;
            animator.SetTrigger("ShootUpR");
        }
        else if (_shootingStance == ShootingStance.Backwards)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,180);
            spawnPosition.position = transform.position + new Vector3(-bulletSpawnRange, bulletSpawnRange, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  Vector2.left * bullet.bulletSpeed;
            animator.SetTrigger("ShootForward");
        }
        else
        {
            Quaternion spawnRotation = quaternion.identity;
            spawnPosition.position = transform.position + new Vector3(bulletSpawnRange, 0, 0);
            Bullet bullet = Instantiate(currentBullet, spawnPosition.position, spawnRotation);
            bullet.rb.velocity =  Vector2.right * bullet.bulletSpeed;
            animator.SetTrigger("ShootForward");
        }
        
        currentBullet.bulletNumber -= 1;
        
        if (currentBullet.bulletNumber <= 0)
        {
            currentBullet.SetBulletValues(standardBulletInfo);
        }
        
        Debug.Log(currentBullet.bulletNumber);
    }
}
