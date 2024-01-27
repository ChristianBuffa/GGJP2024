using UnityEngine;

public class HeavyMachinegun : MonoBehaviour, IPickup
{
    [SerializeField] private GameObject bulletPrefab;

    public void OnPickUp(GameObject bullet)
    {
        Debug.Log("suca");
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
