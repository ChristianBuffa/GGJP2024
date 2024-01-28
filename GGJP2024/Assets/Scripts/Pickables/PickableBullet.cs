using UnityEngine;

public class PickableBullet : MonoBehaviour, IPickup
{
    [SerializeField] private GameObject bulletPrefab;

    public void OnPickUp(GameObject bullet)
    {
        Shooting shooting = FindObjectOfType<Shooting>();
        shooting.currentBullet = bullet.GetComponent<Bullet>();
        shooting.currentBullet.SetBulletValues();
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
