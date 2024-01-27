using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
    public enum ShootingStance
    {
        Forward,
        Up,
        Diagonal
    }
    
    [SerializeField] private Transform spawnPositionForward;
    [SerializeField] private Transform spawnPositionUp;
    [SerializeField] private Transform spawnPositionDiagonal;
    
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
            _shootingStance = ShootingStance.Diagonal;
        }
        else if(Input.GetKey(KeyCode.W))
        {
            _shootingStance = ShootingStance.Up;
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
        if (_shootingStance == ShootingStance.Up)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,90);
            Bullet bullet = Instantiate(currentBullet, spawnPositionUp.position, spawnRotation);
            bullet.rb.velocity =  Vector2.up * bullet.bulletSpeed;
        }
        else if(_shootingStance == ShootingStance.Diagonal)
        {
            Quaternion spawnRotation = Quaternion.Euler(0,0,45);
            Bullet bullet = Instantiate(currentBullet, spawnPositionDiagonal.position, spawnRotation);
            bullet.rb.velocity =  new Vector2(0.5f, 0.5f) * bullet.bulletSpeed;
        }
        else
        {
            Quaternion spawnRotation = quaternion.identity;
            Bullet bullet = Instantiate(currentBullet, spawnPositionForward.position, spawnRotation);
            bullet.rb.velocity =  Vector2.right * bullet.bulletSpeed;
        }
    }
}
