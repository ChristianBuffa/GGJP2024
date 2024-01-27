using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Proiettili", fileName = "Nuovo Proiettile")]
public class BulletInfo : ScriptableObject
{
    public float fireRate;
    public float bulletSpeed;
    public int damage;
}