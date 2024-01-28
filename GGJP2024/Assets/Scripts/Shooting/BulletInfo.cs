using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Proiettili", fileName = "Nuovo Proiettile")]
public class BulletInfo : ScriptableObject
{
    public float FireCooldown;
    public float bulletSpeed;
    public int damage;
    public int bulletNumber;
}