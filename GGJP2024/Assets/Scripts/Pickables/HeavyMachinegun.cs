using UnityEngine;

public class Revolver : MonoBehaviour, IPickup
{
    [SerializeField] private GameObject bulletPrefab;

    public void OnPickUp(GameObject bullet)
    {
        Shooting shooting = FindObjectOfType<Shooting>();
        shooting.currentBullet = bullet.GetComponent<Bullet>();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickUp(bulletPrefab);
        }
    }
}