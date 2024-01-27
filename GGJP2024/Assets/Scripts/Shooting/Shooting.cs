using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

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
    
    [SerializeField] private Transform spawnPosition;
    
    public Bullet currentBullet;

    private ShootingStance _shootingStance;
    
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            _shootingStance = ShootingStance.DiagonalFront;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            _shootingStance = ShootingStance.DiagonalBack;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            _shootingStance = ShootingStance.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _shootingStance = ShootingStance.Down;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            _shootingStance = ShootingStance.Backwards;
        }
        else
        {
            _shootingStance = ShootingStance.Forward;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("suca");
        
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
