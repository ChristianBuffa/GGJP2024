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
    }
    
    [SerializeField] private Transform spawnPositionForward;
    [SerializeField] private Transform spawnPositionUp;
    
    public Bullet currentBullet;

    private ShootingStance _shootingStance;

    private void Start()
    {
        _shootingStance = ShootingStance.Up;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
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
        else
        {
            Quaternion spawnRotation = quaternion.identity;
            Bullet bullet = Instantiate(currentBullet, spawnPositionForward.position, spawnRotation);
            bullet.rb.velocity =  Vector2.right * bullet.bulletSpeed;
        }
    }
}
